using System;
using System.Diagnostics;
using System.Threading.Tasks;

class Program
{
    public static void Main(string[] args)
    {
        //The max number of line
        
        /*test cases:
         * case1: allNumbersFile: 296	719	360	1	448	546	100	676	125	250	968	71	802	212	251	
         * case2: allNumbersFile: 963	807	336	682	646	555	890	537	366	783	102	89	314	899	482	
         * case3: allNumbersFile: 73	4	40	924	918	400	677	522	194	918	492	859	69	923	837	
         * case4: allNumbersFile: 603	71	61	951	395	265	204	284	325	866	154	712	938	42	421	
         * case5: allNumbersFile: 215	462	937	904	572	567	659	317	84	905	236	466	484	146	306	
        */

        string allNumbersFilePath = "C:\\Users\\Таня\\OneDrive\\Documents\\KPI\\прога\\lab_2sem\\Lab1\\Task2\\bin\\Debug\\net6.0\\numbers.txt";
        string maxNumberFilePath = "C:\\Users\\Таня\\OneDrive\\Documents\\KPI\\прога\\lab_2sem\\Lab1\\Task2\\bin\\Debug\\net6.0\\max.txt";
        int maxNumber = 0;
        Random rand = new Random();

        using (StreamWriter sw = File.CreateText(allNumbersFilePath))
        {

        }

        using (StreamWriter sw = File.CreateText(maxNumberFilePath))
        {
        }
        /*test cases:
        * case1: allNumbersFile: 296   719 360	1   448	546	100	676	125	250	968	71	802	212	251	
        *        maxNumberFile: 968
        * case2: allNumbersFile: 963   807	336	682	646	555	890	537	366	783	102	89	314	899	482	
        *        maxNumberFile: 963
        * case3: allNumbersFile: 73	4  40	924	918	400	677	522	194	918	492	859	69	923	837	
        *        maxNumberFile: 924
        * case4: allNumbersFile: 603   71	61	951	395	265	204	284	325	866	154	712	938	42	421	
        *        maxNumberFile: 938
        * case5: allNumbersFile: 215   462	937	904	572	567	659	317	84	905	236	466	484	146	306	
        *        maxNumberFile: 905
       */
    }
}