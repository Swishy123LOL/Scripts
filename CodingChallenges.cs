using UnityEngine;
using System.Collections.Generic;
using System;

public class CodingChallenges : MonoBehaviour
{
    //Various problems from projecteuler.net
    //Just want to challenge myself in coding, also for improving it
    public int num;
    public TextAsset text;
    void Start(){ 
        Debug.Log(FindFraction(num));
        int res = 0;
        for (int i = 2; i < 1000; i++)
        {
            if (FindFraction(i).Length > res) res = i;
        }

        Debug.Log(string.Format("And the champion is... {3}{0} with n = {1} and has {2} digits", res, FindFraction(res), FindFraction(res).Length, Environment.NewLine));
    }

    string FindFraction(int n){
        int rmd = 10;
        int irc = 0;
        int i = 0;
        string rc = "";
        string res = "";
        Dictionary<int, int> d = new Dictionary<int, int>();
        while (rmd != 0)
        {
            i++;
            if (rmd < n) {
                res += '0';
                rmd*=10;
                continue;
            } 
            res += rmd/n; 
            rmd%=n;
            rmd*=10;

            if (d.ContainsKey(rmd)) {
                irc = d[rmd] - 1;
                break;
            }
            d[rmd] = i;
        }

        for (int o = 0; o < res.Length; o++)
        {
            if (o >= irc && irc != 0){
                rc += res[o-1];
            }
        }

        return rc;
    }

    bool IsAbundant(int n){
        return n < FindDivisorsSum(n);
    }

    void RetrunDay(int t1, int d1, int m1, int y1, out int t2, int d2, int m2, int y2){
        //The first variables are used for reference
        //Holy shit I can't believe I got this right...
        t2 = t1;    int d = d1;    int m = m1;    int y = y1;
        while (true)
        {
            if (d == d2 && m == m2 && y == y2) break;
            d++;
            t2 = (t2 == 8)? 2 : ++t2;  
            
            if (y%4 == 0 && y%100 != 0 || y%400 == 0) {
                if (d == 29 && m == 2) { m++; d = 0; }
            }
            else if (y%4 != 0 || y%100 == 0 && y%400 != 0){
                if (d == 28 && m == 2) { m++; d = 0; }
            }
            if (d == 30){
                if (m == 4 || m == 6 || m == 9 || m == 11) { m++; d = 0; }
            }
            else if (d == 31){
                if (m != 4 || m != 6 || m != 9 || m != 11) { m++; d = 0; }
            }

            if (m == 13) { m = 1; y++; }
        }
    }

    string CharToWord(char n){
        string s = "";
        switch (n)
        {
            case '1':
                s+= "one";
                break;
            case '2':
                s+= "two";
                break;
            case '3':
                s+= "three";
                break;
            case '4':
                s+= "four";
                break;
            case '5':
                s+= "five";
                break;
            case '6':
                s+= "six";
                break;
            case '7':
                s+= "seven";
                break;
            case '8':
                s+= "eight";
                break;
            case '9':
                s+= "nine";
                break;
        }

        return s;
    }
    string NumToWord(int n){
        //Since we are working with a number below or equal 1000
        //The method only need to output ...hundred..., ...thousand...
        //Tedious proccess...
        string s = "";
        for (int i = n.ToString().Length - 1; i >= 0; i--)
        {
            switch (i)
            {
                case 3:
                    s+= CharToWord(n.ToString()[n.ToString().Length - i - 1]) + "thousand";
                    break;
                case 2:
                    if (int.Parse(n.ToString()[n.ToString().Length - i - 1].ToString()) == 0) {
                        break;
                    } 
                    s+= CharToWord(n.ToString()[n.ToString().Length - i - 1]) + "hundred";
                    if (n.ToString()[n.ToString().Length - i] != '0') { s+= "and"; }
                    break;
                case 1:
                    switch (n.ToString()[n.ToString().Length - i - 1])
                    {
                        case '1':
                            switch (n.ToString()[n.ToString().Length - i])
                            {
                                case '0':
                                    s+= "ten";
                                    break;
                                case '1':
                                    s+= "eleven";
                                    break;
                                case '2':
                                    s+= "twelve";
                                    break;
                                case '3':
                                    s+= "thirteen";
                                    break;
                                case '4':
                                    s+= "fourteen";
                                    break;
                                case '5':
                                    s+= "fifteen";
                                    break;
                                case '6':
                                    s+= "sixteen";
                                    break;
                                case '7':
                                    s+= "seventeen";
                                    break;
                                case '8':
                                    s+= "eighteen";
                                    break;
                                case '9':
                                    s+= "nineteen";
                                    break;              
                            }

                            break;
                        case '2':
                            s+= "twenty";
                            break;
                        case '3':
                            s+= "thirty";
                            break;
                        case '4':
                            s+= "forty";
                            break;
                        case '5':
                            s+= "fifty";
                            break;
                        case '6':
                            s+= "sixty";
                            break;
                        case '7':
                            s+= "seventy";
                            break;
                        case '8':
                            s+= "eighty";
                            break;
                        case '9':
                            s+= "ninety";
                            break;
                    }
                    break;
                case 0:
                    if (n.ToString().Length > 1) {
                        if (n.ToString()[n.ToString().Length - i - 2] == '1') { break; }
                        if (n.ToString()[n.ToString().Length - i - 2] == '0' && n.ToString()[n.ToString().Length - i - 1] != '0') {
                            s+= "and";
                        } 
                    }
                    s+= CharToWord(n.ToString()[n.ToString().Length - i - 1]);
                    break;

            }
        }

        return s;
    }
    int FindMultipe(int n){
        //If we list all the natural numbers below 10 that are multiples of 3 or 5, we get 3, 5, 6 and 9. The sum of these multiples is 23.
        //Find the sum of all the multiples of 3 or 5 below 1000.
        int s = 0;
        for (int i = 0; i < n; i++)
        {
            if (i%3 == 0){ s+=i; continue;}
            if (i%5 == 0){ s+=i; }
        }
        return s;
    }

    int PrimeFactor(long n){
        int a = 2;
        long b = n;
        while (n>a/2)
        {
            if (n%a == 0){ n/=a; b=a; }
            else { a++; }
        }

        return (int)b;
    }

    List<long> FindPrime(long n){
        int a = 2;
        int c = a;
        bool[] b = new bool[n];
        List<long> d = new List<long>();


        for (long i = 2; i < Mathf.Sqrt(n); i++)
        {
            if (b[i]==false){
                for (long o = i*i; o < n; o+=i)
                {
                    b[o] = true;
                }
            }
        }

        for (long i = 2; i < n; i++)
        {
            if (b[i]==false){d.Add(i);}
        }

        return d;
    }

    int FindDivisorsCount(long n){
        int a = 0;
        for (int i = 1; i < Mathf.Sqrt(n); i++)
        {
            if (n%i == 0) {
                if (n/i == i) {a++; break;}
                else {a+=2;}
            }
        }

        return a;
    }

    long FindDivisorsSum(long n){
        long a = 0;
        for (int i = 1; i < Mathf.Sqrt(n); i++)
        {
            if (n%i == 0) {
                if (n/i == i) {a+= i; break;}
                else {a+= i + n/i;}
            }
        }

        return a - n;        
    }

    long ReturnTriangular(int n){
        int res = 0;
        for (int i = 1; i < n+1; i++)
        {
            res+=i;
        }

        return res;
    }

    long LatticePaths(int a, int b, Dictionary<string, long> c){
        string key = a + "," + b;

        if (c.ContainsKey(key)) { return c[key]; }
        if (a==0||b==0) { return 1; }

        c[key] = LatticePaths(a-1, b, c) + LatticePaths(a, b-1, c);
        return c[key];
    }

    long CoinSums(int n, int[] a, int b, Dictionary<int, long> c, string d){
        //This was a failure! Move on
        n-=b;

        //Debug.Log(d);

        if (c.ContainsKey(n)) { return 0; }
        if (n < 0) return 0;
        else if (n == 0) return 1;

        long res = 0;

        for (int i = 0; i < a.Length; i++)
        {
            res += CoinSums(n, a, a[i], c, d + a[i].ToString());
        }


        c[n] = res;
        return c[n];
    }

    long CollatzSequence(long n, long b){
        b++; if (n == 1) { return b; }
        n = (n%2 == 0)? n/2 : 3*n + 1; 

        return CollatzSequence(n, b);
    }
}