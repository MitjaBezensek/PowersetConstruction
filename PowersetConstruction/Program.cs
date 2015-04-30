using System.Collections.Generic;
using PowersetConstruction;

namespace NFSM{
   internal class Program{
      private static void Main(){
         var Q = new List<string>{"q0", "q1", "q2", "q3"};
         var Sigma = new List<char>{'0', '1'};
         var Delta = new List<Transition>{
            new Transition("q0", '0', "q0"),
            new Transition("q0", '1', "q0"),
            new Transition("q0", '1', "q1"),
            new Transition("q1", '0', "q2"),
            new Transition("q1", '1', "q2"),
            new Transition("q2", '0', "q3"),
            new Transition("q2", '1', "q3"),
         };
         var Q0 = new List<string>{"q0"};
         var F = new List<string>{"q3"};
         var ndfsm = new NDFSM(Q, Sigma, Delta, Q0, F);
         var dfsm = PowersetConstruction.PowersetConstruction.Convert(ndfsm);
      }
   }
}