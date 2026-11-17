// This host contains synthetic test state, not a reimplementation of TeamRefreshGate.
// Run is called only after the wrapper checks the pinned ZIP, setup and app digests.
using System;
using System.Collections.Generic;
using System.Reflection;

namespace AiubPublicGateKit
{
    public sealed class CaseResult
    {
        public string Id;
        public bool Passed;
        public string Expected;
        public object Observed;
    }

    public sealed class ProbeResult
    {
        public bool AppLoaded;
        public bool Passed;
        public CaseResult[] Cases;
    }

    public static class Probe
    {
        private sealed class SyntheticView
        {
            internal string Snapshot = "prior-snapshot";
            internal bool Busy;
            internal string Error;
            internal int ClearCalls;
            internal int ResultCalls;
            internal void Clear() { Snapshot = null; Busy = true; Error = null; ClearCalls++; }
            internal void Success(string value) { Snapshot = value; Busy = false; Error = null; ResultCalls++; }
            internal void Failure(string value) { Error = value; Busy = false; ResultCalls++; }
        }

        private static object State(SyntheticView view)
        {
            return new { snapshot = view.Snapshot, busy = view.Busy, error = view.Error,
                clearCallbacks = view.ClearCalls, resultCallbacks = view.ResultCalls };
        }

        private static void Add(List<CaseResult> cases, string id, bool passed, string expected, object observed)
        {
            cases.Add(new CaseResult { Id = id, Passed = passed, Expected = expected, Observed = observed });
        }

        public static ProbeResult Run(byte[] applicationBytes)
        {
            var result = new ProbeResult();
            var cases = new List<CaseResult>();
            string stage = "load-app";
            try
            {
                Assembly app = Assembly.Load(applicationBytes);
                result.AppLoaded = true;
                stage = "inspect-gate-contract";
                Type type = app.GetType("AIUsageBar.TeamRefreshGate", true, false);
                const BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.DeclaredOnly;
                ConstructorInfo ctor = type.GetConstructor(flags | BindingFlags.Public, null, Type.EmptyTypes, null);
                MethodInfo begin = type.GetMethod("Begin", flags, null, new Type[] { typeof(Action) }, null);
                MethodInfo apply = type.GetMethod("Apply", flags, null, new Type[] { typeof(long), typeof(Action) }, null);
                MethodInfo close = type.GetMethod("Close", flags, null, Type.EmptyTypes, null);
                if (!type.IsSealed || type.BaseType != typeof(object) || type.TypeInitializer != null || ctor == null ||
                    begin == null || begin.ReturnType != typeof(long) || apply == null || apply.ReturnType != typeof(bool) ||
                    close == null || close.ReturnType != typeof(void))
                    throw new InvalidOperationException("Unexpected gate contract");

                stage = "construct-gate";
                object gate = ctor.Invoke(new object[0]);
                Func<Action, long> Begin = clear => (long)begin.Invoke(gate, new object[] { clear });
                Func<long, Action, bool> Apply = (request, callback) => (bool)apply.Invoke(gate, new object[] { request, callback });
                var view = new SyntheticView();

                stage = "run-synthetic-callbacks";
                long first = Begin(view.Clear);
                Add(cases, "begin-clears-snapshot", first > 0 && view.Snapshot == null && view.Busy && view.Error == null && view.ClearCalls == 1,
                    "Beginning a request immediately clears prior synthetic data and marks it busy.",
                    new { request = first, state = State(view) });

                bool firstApplied = Apply(first, () => view.Success("first-snapshot"));
                Add(cases, "current-success-applies", firstApplied && view.Snapshot == "first-snapshot" && !view.Busy && view.Error == null && view.ResultCalls == 1,
                    "The current request's real gate invokes its callback exactly once for this Apply call.",
                    new { accepted = firstApplied, state = State(view) });

                long second = Begin(view.Clear);
                bool oldSuccess = Apply(first, () => view.Success("stale-success"));
                bool oldError = Apply(first, () => view.Failure("stale-error"));
                bool zero = Apply(0, () => view.Success("invalid-request"));
                bool stayedEmpty = second != first && view.Snapshot == null && view.Busy && view.Error == null && view.ClearCalls == 2 && view.ResultCalls == 1;
                object stateAfterStale = State(view);
                bool secondApplied = Apply(second, () => view.Success("new-snapshot"));
                bool lateOld = Apply(first, () => view.Success("late-stale-success"));
                Add(cases, "superseded-callbacks-rejected", !oldSuccess && !oldError && !zero && stayedEmpty && secondApplied && !lateOld &&
                    view.Snapshot == "new-snapshot" && !view.Busy && view.Error == null && view.ResultCalls == 2,
                    "Old success/error and zero requests cannot alter cleared or newer state, including busy/error fields.",
                    new { oldSuccessAccepted = oldSuccess, oldErrorAccepted = oldError, zeroAccepted = zero,
                        stateAfterStale = stateAfterStale, newAccepted = secondApplied, lateOldAccepted = lateOld, state = State(view) });

                long failed = Begin(view.Clear);
                bool currentFailure = Apply(failed, () => view.Failure("synthetic-read-failure"));
                bool failedEmpty = view.Snapshot == null && !view.Busy && view.Error == "synthetic-read-failure" && view.ResultCalls == 3;
                object failedState = State(view);
                long recovery = Begin(view.Clear);
                bool recoveryCleared = view.Snapshot == null && view.Busy && view.Error == null;
                object recoveryStartState = State(view);
                bool recovered = Apply(recovery, () => view.Success("recovered-snapshot"));
                bool lateError = Apply(failed, () => view.Failure("late-stale-error"));
                Add(cases, "current-failure-and-recovery", currentFailure && failedEmpty && recoveryCleared && recovered && !lateError &&
                    view.Snapshot == "recovered-snapshot" && !view.Busy && view.Error == null && view.ResultCalls == 4,
                    "A current failure leaves an empty snapshot; a later request recovers without an older failure overwriting it.",
                    new { failureAccepted = currentFailure, failedState = failedState, recoveryStartState = recoveryStartState,
                        recoveryAccepted = recovered, lateErrorAccepted = lateError, state = State(view) });

                close.Invoke(gate, new object[0]);
                int clearsBefore = view.ClearCalls;
                bool afterClose = Apply(recovery, () => view.Success("after-close"));
                long closedBegin = Begin(view.Clear);
                bool closedZero = Apply(closedBegin, () => view.Failure("after-close-error"));
                Add(cases, "closed-gate-rejects-work", !afterClose && closedBegin == 0 && !closedZero && view.ClearCalls == clearsBefore &&
                    view.ResultCalls == 4 && view.Snapshot == "recovered-snapshot" && !view.Busy && view.Error == null,
                    "Close rejects pending callbacks and a subsequent Begin without invoking either callback.",
                    new { afterCloseAccepted = afterClose, requestAfterClose = closedBegin, closedZeroAccepted = closedZero, state = State(view) });
                result.Passed = cases.Count == 5 && cases.TrueForAll(x => x.Passed);
            }
            catch (Exception error)
            {
                // Keep portable output free of paths or environment-specific exception data.
                Add(cases, "probe-execution", false, "The fixed gate contract can be inspected and invoked.",
                    new { appLoaded = result.AppLoaded, stage = stage, error = "gate_contract_or_invocation_failed", exceptionType = error.GetType().Name });
                result.Passed = false;
            }
            result.Cases = cases.ToArray();
            return result;
        }
    }
}
