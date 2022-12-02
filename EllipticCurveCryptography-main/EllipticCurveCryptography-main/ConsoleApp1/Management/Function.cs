using ConsoleApp1.Entities;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace ConsoleApp1.Management
{
    class Function
    {
		private List<KiTu> lstKiTu = new List<KiTu>();
		private List<ToaDoMa> lstToaDoMa = new List<ToaDoMa>();
		private List<ToaDo> lstToaDoRo = new List<ToaDo>();
		private double phi, a, b;
		private int p;
		private ToaDo G;

		public Function()
		{

		}

		public Function(double a, double b, int p, ToaDo g)
		{

			this.a = a;
			this.b = b;
			this.p = p;
			G = g;
		}

		ToaDo nhan(ToaDo A, int d)
		{
			ToaDo P = A;
			/*	Thread t1 = new Thread(() =>
				{
					for (int i = 1; i < d; i++)
					{
						Thread t = new Thread(()=> P = cong(A, P));
						t.Start();
						t.Join();
					}

				});
				t1.Start();
				t1.Join();*/
			for (int i = 1; i < d; i++)
			{
				P = cong(A, P);
				
			}
			return P;
		}

		// Ham tinh mod p
		double mod(double a, double p)
		{
			double b = a % p;
			if (b >= 0)
				return b;
			else
				return (p + b);
		}

		// Ham tinh R=P+Q trong he toa do Affine voi Z=n
		ToaDo cong(ToaDo P, ToaDo Q)
		{
			ToaDo R = new ToaDo();
			if (P.getX() == 0 && P.getY() == 0)
				return Q;
			if (Q.getX() == 0 && Q.getY() == 0)
				return P;
			if (P.getX() == Q.getX())
			{
				if (P.getY() == Q.getY())
					phi = chia(3 * P.getX() * P.getX() + a, 2 * P.getY(), p);
				if (P.getY() == mod(-Q.getY(), p))
				{
					R.setX(0);
					R.setY(0);
					return R;
				}
			}
			else
				phi = chia(Q.getY() - P.getY(), Q.getX() - P.getX(), p);
			R.setX(mod(phi * phi - P.getX() - Q.getX(), p));
			R.setY(mod(phi * (P.getX() - R.getX()) - P.getY(), p));
			return R;
		}

		// Ham tinh phep chia mod
		double chia(double tu, double mau, double p)
		{
			int t = 1;
			if (mau < 0)
			{
				mau = Math.Abs(mau);
				tu = -tu;
			}
			while ((mau * t) % p != 1)
			{
				t++;
			}
			if (tu >= 0)
				return (tu * t) % p;
			else
				return (p + (tu * t) % p);
		}

		/*public void sinhdiem()
		{
			// Character[] table = new Character[200];
			int i = 0;
			for (int j = 32; j <= 126; j++)
			{
				KiTu kt = new KiTu();
				kt.setCharactr((char)j);
				kt.setToado(nhan(G, i + 1));
				lstKiTu.Add(kt);
				i++;
			}

		}*/

		public void khoitao()
		{
			for (int i = 32; i <= 126; i++)
			{
				this.lstKiTu.Add(new KiTu());
			}
		}

		public void sinhkt()
		{
			int i = 32;
			foreach (KiTu ls in lstKiTu)
			{
				ls.setCharactr((char)i);
				i++;
			}
		}

		public void sinhtd()
		{
			int i = 1;
			foreach (KiTu ls in lstKiTu)
			{
				ls.setToado(nhan(G, i));
				i++;
			}
		}
		void mahoaECC(int nB, ToaDo Pm, int k)
		{
			ToaDo Pb = new ToaDo();
			Pb = nhan(G, nB);

			ToaDo C1 = new ToaDo();
			ToaDo C2 = new ToaDo();
			C1 = nhan(G, k);
			C2 = cong(Pm, nhan(Pb, k));
			ToaDoMa ma = new ToaDoMa(C1, C2);
			lstToaDoMa.Add(ma);

		}

		public void mahoachuoi(String str, int key, int k)
		{
			for (int j = 0; j < str.Length; j++)
			{
				for (int i = 0; i <= 94; i++)
				{
					if (str[j] == lstKiTu[i].getCharctr())
					{
						ToaDo Pm = new ToaDo();
						Pm = lstKiTu[i].getToado();
						mahoaECC(key, Pm, k);
					}
				}
			}
		}
		void giaimaECC(ToaDo C1, ToaDo C2, int nB)
		{

			ToaDo C3 = new ToaDo();
			C3 = nhan(C1, nB);
			C3.setY( mod(-C3.getY(), p));
			ToaDo C = new ToaDo();
			C = cong(C2, C3);

			lstToaDoRo.Add(C);
			/*Console.Write(dichma(C));
			Console.Write("(" + C.x + "," + C.y + ") ");*/

		}
		public void giaimachuoi(string str,int key)
		{
			foreach (ToaDoMa ls in xulichuoi(str))
			{
				giaimaECC(ls.getC1(),ls.getC2(),key);

			}
		}
		List<ToaDoMa> xulichuoi(string input)
        {
			string[] numbers = Regex.Split(input, @"\D+");
			List<ToaDoMa> a = new List<ToaDoMa>();
			if (numbers.Length % 4 == 0)
			{
				
				for (int i = 0; i < numbers.Length; i += 4)
				{
					ToaDo C1 = new ToaDo(double.Parse(numbers[i]), double.Parse(numbers[i + 1]));
					ToaDo C2 = new ToaDo(double.Parse(numbers[i + 2]), double.Parse(numbers[i + 3]));
					ToaDoMa toadoma = new ToaDoMa(C1, C2);
					a.Add(toadoma);
				}
				
            }
            else
            {
				Console.WriteLine("INCORRECT!!!");
            }
			return a;
		}
		char dichma(ToaDo C)
		{
			char kitu = new char();
			foreach (KiTu ls in lstKiTu)
			{
				if (ls.getToado().getX() == C.getX() && ls.getToado().getY() == C.getY())
				{
					kitu = ls.getCharctr();
					break;
				}

			}
			return kitu;
		}
		public void displayBKT()
		{
			foreach (KiTu ls in lstKiTu)
				Console.WriteLine(ls);
		}

		public void displayMa()
		{
			foreach (ToaDoMa ls in lstToaDoMa)
				Console.Write(ls);
			Console.WriteLine();
			lstToaDoMa = null;
			System.GC.Collect();
			lstToaDoMa = new List<ToaDoMa>();
		}

		public void displayRo()
		{
			Console.WriteLine("COORDINATE: ");
			foreach (ToaDo ls in lstToaDoRo)
				Console.Write(ls);
			Console.WriteLine();
			Console.WriteLine("PLAINTEXT: ");
			foreach (ToaDo ls in lstToaDoRo)
				Console.Write(dichma(ls));
			lstToaDoRo = null;
			System.GC.Collect();
			lstToaDoRo = new List<ToaDo>();
		}
	}
}
