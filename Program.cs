using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

public class devsSystem
{
	static void Main(string[] args)
	{
		MenuMain();
	}
	static void MenuMain()
	{
		Console.Write(globalVar.fMenu);
		int code;
		string s = Console.ReadLine();
		if (!Int32.TryParse(s, out code)) { MenuMain(); }
		switch (code)
		{
			case 0:
				Environment.Exit(0);
				break;
			case 1:
				MenuDev();
				break;
			case 2:
				MenuUser();
				break;
			default:
				MenuMain();
				break;
		}
	}
	static void MenuUser()
	{
		Console.Write(globalVar.tMenu);
		int code;
		string s = Console.ReadLine();
		if (!Int32.TryParse(s, out code)) { MenuUser(); }
		switch (code)
		{
			case 0:
				MenuMain();
				break;
			case 1:
				NewUser();
				MenuUser();
				break;
			case 2:
				SwUser();
				break;
			case 3:
				AllUser();
				MenuUser();
				break;
			default:
				MenuUser();
				break;
		}
	}
	static void NewUser()
	{
		using (StreamWriter sw = new StreamWriter(globalVar.pathx, true, System.Text.Encoding.Default))
		{
			Console.Write("Никнейм: ");
			string nick = Console.ReadLine();
			Console.Write("Код доступа: ");
			string passw = Console.ReadLine();
			sw.WriteLine(String.Join(" | ", new string[] { nick, passw }));
		}
	}

	static void SwUser()
	{
		Console.WriteLine("Введите имя работника: ");
		Console.ReadLine();
		Console.WriteLine("Введите код доступа: ");
		Console.ReadLine();
	}
	static void AllUser()
	{
		Console.WriteLine(new string('*', globalVar.W / 2 - 7) + " Работники " +
						  new string('*', globalVar.W / 2 - 7));

		using (StreamReader us = new StreamReader(globalVar.pathx, System.Text.Encoding.Default))
		{
			string user;
			while ((user = us.ReadLine()) != null)
			{
				user = user.Split('|')[0];
				Console.WriteLine('*' + new string(' ', globalVar.W / 2 - 19) + "* " + user + '*' +
								  new string(' ', globalVar.W / 2 + 8) + '*');
				Console.WriteLine(new string('*', globalVar.W));
			}
		}

	}
	static void MenuDev()  // Меню выбора взаимодействия с устройствами
	{
		Console.Write(globalVar.thMenu);
		int code;
		string s = Console.ReadLine();
		if (!Int32.TryParse(s, out code)) { MenuDev(); }
		switch (code)
		{
			case 0:
				MenuMain();
				break;
			case 1:
				Searchdev();
				MenuDev();
				break;
			case 2:
				Alldevs();
				MenuDev();
				break;
			case 3:
				MenuEdit();
				break;
			default:
				MenuDev();
				break;
		}
	}
	static void MenuEdit()   // Меню редактирования устройства
	{
		Console.Write(globalVar.foMenu);
		int code;
		string s = Console.ReadLine();
		if (!Int32.TryParse(s, out code)) { MenuEdit(); }
		switch (code)
		{
			case 0:
				MenuDev();
				break;
			case 1:
				AddDev();
				MenuEdit();
				break;
			case 2:
				//Removedev();
				MenuEdit();
				break;
			default:
				MenuEdit();
				break;
		}
	}
	static void Alldevs()   // Список устройств
	{


		Console.WriteLine(new string('*', globalVar.W / 2 - 3) + " Девайсы " +
						  new string('*', globalVar.W / 2 - 4));
		string brend = "Название";
		string price = "Цена";
		string garant = "Гарантия";
		string year = "Год выпуска";
		Console.WriteLine(MakedevStr(new string[] { brend, price, garant, year }));
		Console.WriteLine(new string('*', globalVar.W));
		using (StreamReader ree = new StreamReader(globalVar.path, System.Text.Encoding.Default))
		{
			string dev;
			while ((dev = ree.ReadLine()) != null)
			{
				string[] devinf = dev.Split('|');
				Console.WriteLine(MakedevStr(devinf));
				Console.WriteLine(new string('*', globalVar.W));
			}
		}

	}


	static void AddDev()    // Добавление устройства
	{

		using (StreamWriter sw = new StreamWriter(globalVar.path, true, System.Text.Encoding.Default))
		{
			Console.Write("Название: ");
			string brend = Console.ReadLine();
			Console.Write("Цена: ");
			string price = Console.ReadLine();
			Console.Write("Гарантия: ");
			string garant = Console.ReadLine();
			Console.Write("Год выпуска: ");
			string year = Console.ReadLine();
			sw.WriteLine(String.Join(" | ", new string[] { brend, price, garant, year }));
		}
	}

	static string MakedevStr(string[] s)
	{
		return ("* " +
			   s[0] + new string(' ', globalVar.W / 4 - s[0].Length - 2) + "* " +
			   s[1] + new string(' ', globalVar.W / 4 - s[1].Length - 2) + "* " +
			   s[2] + new string(' ', globalVar.W / 4 - s[2].Length - 2) + "* " +
			   s[3] + new string(' ', globalVar.W / 4 - s[3].Length - 1) + '*');
	}
	static void Searchdev()
	{
		Console.Write("Введите название устройства: "); // поиск производится по всем характеристикам
		string data = Console.ReadLine();
		string[] indevs = SearchingDev(data);

		if (indevs.Length == 0)
		{
			Console.WriteLine("Не найдено.");
			return;
		}
		Console.WriteLine(new string('*', globalVar.W));
		foreach (string dev in indevs)
		{
			Console.WriteLine('*' + dev);
			Console.WriteLine(new string('*', globalVar.W));
		}
	}
	static string[] SearchingDev(string data)
	{

		string[] devs = File.ReadAllLines(globalVar.path);
		List<string> intinDev = new List<string>();
		for (int i = 0; i < devs.Length; i++)
		{
			if (devs[i].Contains(data))
			{
				intinDev.Add(devs[i]);
			}
		}
		return intinDev.ToArray();
	}
}


public static class globalVar
{
	public static int W = Console.WindowWidth; // Ширина терминала
	public static string path = @"d:\phone.txt";
	public static string pathx = @"d:\phonex.txt";

	// дать соответствующие имена переменным меню

	public static string fMenu = "\n1. Девайсы. " +
								 "2. Работники.\n" +
								 "0. Выход.\n" + "Введите пункт меню: ";


	public static string tMenu = "\n1. Добавить работника. " +
								 "2. Сменить работника. " +
								 "3. Вывести всех работников. " +
								 "0. Вернуться.\nВведите пункт меню: ";


	public static string thMenu = "\n1. Найти устройство. " +
								  "2. Вывести все устройства. " +
								  "3. Изменить устройства. " +
								  "0. Вернуться.\nВведите пункт меню: ";


	public static string foMenu = "\n1. Добавить устройство " +
								  "2. Удалить устройство. " +
								  "0. Вернуться.\nВведите пункт меню: ";
}


