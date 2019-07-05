using System;
using System.Linq;
using System.Collections.Generic;

namespace Tavisca.Bootcamp.LanguageBasics.Exercise1
{
    public static class Program
    {
        static void Main(string[] args)
        {
            Test(
                new[] { 3, 4 }, 
                new[] { 2, 8 }, 
                new[] { 5, 2 }, 
                new[] { "P", "p", "C", "c", "F", "f", "T", "t" }, 
                new[] { 1, 0, 1, 0, 0, 1, 1, 0 });
            Test(
                new[] { 3, 4, 1, 5 }, 
                new[] { 2, 8, 5, 1 }, 
                new[] { 5, 2, 4, 4 }, 
                new[] { "tFc", "tF", "Ftc" }, 
                new[] { 3, 2, 0 });
            Test(
                new[] { 18, 86, 76, 0, 34, 30, 95, 12, 21 }, 
                new[] { 26, 56, 3, 45, 88, 0, 10, 27, 53 }, 
                new[] { 93, 96, 13, 95, 98, 18, 59, 49, 86 }, 
                new[] { "f", "Pt", "PT", "fT", "Cp", "C", "t", "", "cCp", "ttp", "PCFt", "P", "pCt", "cP", "Pc" }, 
                new[] { 2, 6, 6, 2, 4, 4, 5, 0, 5, 5, 6, 6, 3, 5, 6 });
            Console.ReadKey(true);
        }

        private static void Test(int[] protein, int[] carbs, int[] fat, string[] dietPlans, int[] expected)
        {
            var result = SelectMeals(protein, carbs, fat, dietPlans).SequenceEqual(expected) ? "PASS" : "FAIL";
            Console.WriteLine($"Proteins = [{string.Join(", ", protein)}]");
            Console.WriteLine($"Carbs = [{string.Join(", ", carbs)}]");
            Console.WriteLine($"Fats = [{string.Join(", ", fat)}]");
            Console.WriteLine($"Diet plan = [{string.Join(", ", dietPlans)}]");
            Console.WriteLine(result);
        }

        public static int[] SelectMeals(int[] protein, int[] carbs, int[] fat, string[] dietPlans)
        {
            
            //array that will store the output indexes
            int[] output_index = new int[dietPlans.Length];

            //calculting the calorie array using the formula given
            int[] calories = new int[protein.Length];
            for(int i=0; i<protein.Length; i++){
                calories[i] = fat[i]*9 + carbs[i]*5 + protein[i]*5;
            }

            //itrate through every dietPlan and add respective index to the output_index array
            for(int i=0;i<dietPlans.Length; i++){
                
                string local_diet_plan = dietPlans[i];

                if(local_diet_plan.Length == 0){

                    output_index[i]=0;  

                }else{
                    /*
                        key here is, we don't have to search for a max/min starting from index 0
                        but the indexes defined in an candidate_indices array.
                        initially candidate_indices contain all the indexes
                     */

                    List<int> candidate_indices = new List<int>();
                    for(int j=0;i<protein.Length;j++){
                        candidate_indices.Add(j);
                    }

                    foreach(char ch in local_diet_plan){
                        switch(ch){
                                
                            case 'P':
                                
                                candidate_indices = updateCandidateIndices(protein, candidate_indices, 1);
                                break;
                            case 'p':
                                candidate_indices = updateCandidateIndices(protein, candidate_indices, 0);
                                break;
                            case 'C':
                                candidate_indices = updateCandidateIndices(carbs, candidate_indices, 1);
                                break;
                            case 'c':
                                candidate_indices = updateCandidateIndices(carbs, candidate_indices, 0);
                                break;
                            case 'F':
                                candidate_indices = updateCandidateIndices(fat, candidate_indices, 1);
                                break;
                            case 'f':
                                candidate_indices = updateCandidateIndices(fat, candidate_indices, 0);
                                break;
                            case 'T':
                                candidate_indices = updateCandidateIndices(calories, candidate_indices, 1);
                                break;
                            case 't':
                                candidate_indices = updateCandidateIndices(calories, candidate_indices, 0);
                                break;
                        }
                    }

                    output_index[i] = candidate_indices[0];

                }
                return output_index;
            }


            
        }

        public static List<int> updateCandidateIndices(int[] arr, List<int> c_index, int flag)
        {
            
            int element = arr[c_index[0]];

            if(c_index.Count>0){
                if(flag == 1){
                    //adding maximum value to element
                    for(int i =1;i<c_index.Count;i++)
                        if(element<arr[c_index[i]]) element = arr[c_index[i]];                          
                }else{
                    //adding minimum value to element
                    for(int i =1;i<c_index.Count;i++)
                        if(element>arr[c_index[i]]) element = arr[c_index[i]];
                }
            }else{
                return c_index;
            }
            

            //updating the candidate_indices according to the occrance of element in nutrition_array
            List<int> temp = new List<int>();
            foreach(int i in c_index){
                if(arr[i]==element) temp.Add(i);
            }

            return temp;
        }
    }
}
