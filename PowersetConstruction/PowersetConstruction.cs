using System.Collections.Generic;
using System.Linq;

namespace PowersetConstruction{
   internal class PowersetConstruction{
      public static DFSM Convert(NDFSM nd){
         var Q = new List<string>();
         var Sigma = nd.Sigma.ToList();
         var Delta = new List<Transition>();
         var Q0 = nd.Q0.ToList();
         var F = new List<string>();

         var processed = new List<string>();
         var queue = new Queue<string>(Q0);

         while (queue.Count > 0){
            var setState = queue.Dequeue();
            processed.Add(setState);
            Q.Add(setState);

            var statesInCurrentSetState = setState.Split(',').ToList();
            foreach (var state in statesInCurrentSetState){
               if (nd.F.Contains(state)){
                  F.Add(setState);
                  break;
               }
            }
            var symbols = nd.Delta
               .Where(t => statesInCurrentSetState.Contains(t.StartState))
               .Select(t => t.Symbol)
               .Distinct();
            foreach (var symbol in symbols){
               var reachableStates =
                  nd.Delta
                     .Where(t => t.Symbol == symbol &&
                                 statesInCurrentSetState.Contains(t.StartState))
                     .OrderBy(t => t.EndState).
                     Select(t => t.EndState);
               var reachableSetState = string.Join(",", reachableStates);

               Delta.Add(new Transition(setState, symbol, reachableSetState));

               if (!processed.Contains(reachableSetState)){
                  queue.Enqueue(reachableSetState);
               }
            }
         }
         return new DFSM(Q, Sigma, Delta, Q0, F);
      }
   }
}