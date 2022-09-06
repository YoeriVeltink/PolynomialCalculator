using System;
using System.Collections.Generic;
namespace PolynomialApp {
      
    class Calculations {
          
        
        static void Main(string[] args) {
            

            Monomial[] polynomial1 = new Monomial[] {new Monomial(1, 2), new Monomial(1, 1), new Monomial(5, 0)}; // x^2 + x + 5
            Monomial[] polynomial2 = new Monomial[] {new Monomial(2, 3), new Monomial(1, 2), new Monomial(5, 1),new Monomial(3, 0)}; // 2x^3 + x^2 + 5x + 3 

            Monomial[] resultPolynomial = Consolidate(Multiply(polynomial1,polynomial2));
            foreach(Monomial m in resultPolynomial)
                m.GenerateString();
            string outputString = "";
            for(int i = 0; i < resultPolynomial.Length; i++){
                if(i == resultPolynomial.Length-1){
                    outputString+=resultPolynomial[i].mString;
                }
                else{
                    outputString+=resultPolynomial[i].mString;
                    outputString+="+";
                }
                    
            }
            Console.WriteLine(outputString);
            

        }

        //Multiplies 2 polynomials, p1 and p2
        static List<Monomial> Multiply(Monomial[] p1, Monomial[] p2){
            List<Monomial> multiplyResult = new List<Monomial>();
            foreach(Monomial m1 in p1){
                foreach(Monomial m2 in p2){
                    multiplyResult.Add(new Monomial(m1.coefficient * m2.coefficient, m1.exponent + m2.exponent));
                }
            }

            return multiplyResult;
        }

        static Monomial[] Consolidate(List<Monomial> polynomial){
            List<Monomial> consolidationResult = new List<Monomial>();
            List<List<Monomial>> subgroups = new List<List<Monomial>>();
            foreach(Monomial m in polynomial){
                if(m.coefficient == 0){
                    polynomial.Remove(m);
                }
            }
            while(polynomial.Count > 0){
                List<Monomial> subgroup = new List<Monomial>();
                int searchExponent = polynomial[0].exponent;
                for(int i = 0; i<polynomial.Count; i++){
                    if(polynomial[i].exponent == searchExponent){
                        subgroup.Add(polynomial[i]);
                    }
                }
                foreach(Monomial subgroupMember in subgroup){
                    polynomial.Remove(subgroupMember);
                }
                subgroups.Add(subgroup);
            }
            foreach(List<Monomial> subgroup in subgroups){
                int coefficientSum=0;
                int exponent = subgroup[0].exponent;
                foreach(Monomial m in subgroup){
                    coefficientSum+=m.coefficient;
                }
                consolidationResult.Add(new Monomial(coefficientSum, exponent));
            }
            return consolidationResult.ToArray();
        }
    }

    public class Monomial {
        string[] superscripts = new string[] {"\u00B2","\u00B3","\u2074","\u2075","\u2076","\u2077","\u2078","\u2079"};
        public int coefficient {get; private set;}
        public int exponent {get; private set;}
        public string mString {get; private set;}

        public Monomial(int coefficient, int exponent){
            this.coefficient = coefficient;
            this.exponent = exponent;
        }
        
        public void GenerateString(){
            if(exponent == 0){
                mString = coefficient.ToString();
                return;
            }  
            if(coefficient != 1)
                mString += coefficient;
            mString += "x";
            if(exponent!= 1)
                mString+=superscripts[exponent-2];
        }

        
    }
}
