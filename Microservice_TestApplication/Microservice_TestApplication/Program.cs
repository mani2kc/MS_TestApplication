using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microservice_TestApplication.GetEstimate;
using Microservice_TestApplication.GetTaxes;

namespace Microservice_TestApplication
{
    class Program
    {
        static GetEstimateClient getEstimate = new GetEstimateClient("BasicHttpBinding_IGetEstimate");
        static GetTaxesClient getTaxes = new GetTaxesClient("BasicHttpBinding_IGetTaxes");

        static void Main(string[] args)
        {
            while (true) // Loop indefinitely
            {
                Console.WriteLine("*******************************************************************************");
                Console.WriteLine("Enter input ==> 1 for GetEstimate ==> 2 for GetTaxes ==> anykey to exit the application"); // Prompt
                string line = Console.ReadLine(); // Get string from user
                int value = Int32.Parse(line);
                if (value == 1)
                {
                    getSOCEstimate();
                }
                else if (value == 2)
                {
                    getSOCTaxes();
                }
                else // Check string
                {
                    break;
                }
            }

            Console.ReadKey();

        }

        public static void getSOCEstimate()
        {
            List<GetEstimate.ProductInfo> prodlst = new List<GetEstimate.ProductInfo>();

            SOCRequest request = new SOCRequest();

            GetEstimate.ProductInfo prodA = new GetEstimate.ProductInfo();
            prodA.ProductIOSC = "A";
            prodA.Rate = 10.0M;

            GetEstimate.ProductInfo prodB = new GetEstimate.ProductInfo();
            prodB.ProductIOSC = "B";
            prodB.Rate = 20.0M;

            GetEstimate.ProductInfo prodC = new GetEstimate.ProductInfo();
            prodC.ProductIOSC = "C";
            prodC.Rate = 30.0M;

            GetEstimate.ProductInfo prodD = new GetEstimate.ProductInfo();
            prodD.ProductIOSC = "D";
            prodD.Rate = 40.0M;

            prodlst.Add(prodA);
            prodlst.Add(prodB);
            prodlst.Add(prodC);
            prodlst.Add(prodD);

            request.Products = prodlst.ToArray();

            //GetSOCEstimate
            SOCResponse response = getEstimate.GetSOCEstimate(request);

            if (response != null && response.SOCEstimate != null)
            {
                Console.WriteLine("\n");
                Console.WriteLine("SOC Estimate :");
                Console.WriteLine("SOC Monthly charges :" + response.SOCEstimate.Monthly);
                Console.WriteLine("SOC FirstBill charges :" + response.SOCEstimate.FirstBill);

                if (response.SOCEstimate.TaxResponse != null)
                {
                    Console.WriteLine("SOC Monthly Taxes :" + response.SOCEstimate.TaxResponse.MonthlyTaxes);
                    Console.WriteLine("SOC FirstBill Taxes :" + response.SOCEstimate.TaxResponse.FirstBillTaxes);
                }
                Console.WriteLine("\n");
            }
            else
            {
                Console.WriteLine("\n");
                Console.WriteLine("No SOCEstimate for the request");
                Console.WriteLine("\n");
            }

        }

        public static void getSOCTaxes()
        {
            TaxRequest request = new TaxRequest();
            
            
            List<GetTaxes.ProductInfo> prodlst = new List<GetTaxes.ProductInfo>();            

            GetTaxes.ProductInfo prodA = new GetTaxes.ProductInfo();
            prodA.ProductIOSC = "A";
            prodA.Rate = 10.0M;

            GetTaxes.ProductInfo prodB = new GetTaxes.ProductInfo();
            prodB.ProductIOSC = "B";
            prodB.Rate = 20.0M;

            GetTaxes.ProductInfo prodC = new GetTaxes.ProductInfo();
            prodC.ProductIOSC = "C";
            prodC.Rate = 30.0M;

            GetTaxes.ProductInfo prodD = new GetTaxes.ProductInfo();
            prodD.ProductIOSC = "D";
            prodD.Rate = 40.0M;

            prodlst.Add(prodA);
            prodlst.Add(prodB);
            prodlst.Add(prodC);
            prodlst.Add(prodD);

            request.Products = prodlst.ToArray();

            GetTaxes.TaxResponse response = getTaxes.GetSOCTaxes(request);


            if (response != null)
            {
                Console.WriteLine("\n");
                Console.WriteLine("Standalone TaxResponse :");
                Console.WriteLine("Monthly Taxes:" + response.MonthlyTaxes);
                Console.WriteLine("FirstBill Taxes" + response.FirstBillTaxes);
                Console.WriteLine("\n");
            }
            else
            {
                Console.WriteLine("\n");
                Console.WriteLine("No Tax response");
                Console.WriteLine("\n");
            }


        }
    }
}
