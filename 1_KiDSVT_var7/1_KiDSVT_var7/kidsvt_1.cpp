//---------------------------------------------------------------------------

#include <vcl.h>
#include <stdio.h>
#pragma hdrstop

//---------------------------------------------------------------------------

int f(int vid, int val)
{
        if (vid == -1) //ni kakoy
        {
                return val;
        }
        else
        {
                return vid;
        }
}

int or(int vid, int val1, int val2)
{
        if (vid == -1) //ni kakoy
        {
                return val1 || val2;
        }
        else
        {
                return vid;
        }
}

int not(int vid, int val1)
{
        if (vid == -1) //ni kakoy
        {
                return !val1;
        }
        else
        {
                return vid;
        }
}

int and(int vid, int val1, int val2)
{
        if (vid == -1) //ni kakoy
        {
                return val1 && val2;
        }
        else
        {
                return vid;
        }
}

int and3(int vid, int val1, int val2 , int val3)
{
        if (vid == -1) //ni kakoy
        {
                return val1 && val2 && val3;
        }
        else
        {
                return vid;
        }
}

int ftrue(int x[])
{
        int f3 = x[1] || (!x[2]);
        int f1 = !x[3];
        int f2 = x[5] && x[6];
        int f4 = x[4] && f2 && x[7];
        int f5 = f1 || f4;
        int f6 = (!f3) && f5;

        return f6;
}

int ftest(int x[], int n[])
{
        int f3 = or( n[8], f(n[1], x[1]) , (!f(n[2], x[2])) );
        int f1 = not( n[9], f(n[3], x[3]) );
        int f2 = and (n[10] , f(n[5], x[5]) ,  f(n[6], x[6]) );
        int f4 = and3(n[11] , f(n[4], x[4]) , f2 , f(n[7], x[7]));
        int f5 = or( n[12] , f1 , f4);
        int f6 = and (n[13] , !f3 , f5);

        return f6;
}

#pragma argsused
int main(int argc, char* argv[])
{
        int node;
        int type;
        int const nn = 14;
        int nodes[nn];
        int x[8];


        printf("Error node number (1..13): ");
        scanf("%d", &node);
        printf("Error node type (const 1 or 0): ");
        scanf("%d", &type);

        for (int i = 0; i < nn; i++)
        {
                if (i == node)
                {
                        nodes[i] = type;                
                }
                else
                {
                        nodes[i] = -1;
                }
        }

        for (unsigned int i = 0; i < 128; i++)
        {
                unsigned int m = 1;
                for (int j = 1; j <= 7; j++)
                {
                        if (i & m)
                        x[j] = 1;
                        else
                        x[j] = 0;
                        m = m << 1;
                }

                int r1 = ftrue(x);
                int r2 = ftest(x, nodes);

                if (r1 != r2)
                {
                        for (int j = 7; j >= 1; j--)
                        {
                                printf("%d", x[j]);
                        }
                        printf("\n");
                }
        }

        printf("Done!!!");        
        scanf("%d", &node);        

        return 0;
}
//---------------------------------------------------------------------------
