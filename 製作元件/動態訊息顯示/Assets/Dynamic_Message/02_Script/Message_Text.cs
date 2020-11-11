//動態顯示訊息，像是線上遊戲的訊息欄
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic; //使用List

/*
		a.T為一種宣告的類型，可以為物件,字串,變數等,以字串為例:
		List<string> mList = new List<string>();

		1.想要增加元素：List. Add(T item)    添加一個元素
		如：mList.Add("比利王");

		2.插入元素：Insert(int index, T item);    在index位置添加一個元素
		如：mList.Insert(1, "比利王"); 

		3.删除元素:  List. Remove(T item)       删除一個值
		如:List.Remove( "比利王");

		4.List清空：List. Clear ()
		如：mList.Clear();

		5.獲得List中元素数目： List. Count () 
	*/

public class Message_Text : MonoBehaviour {



	//宣告變數=====================================
	//int
	//訊息現在數量
	int String_Quantity = 0;
	//訊息顯示上限
	public int String_Max = 3;

	//測試用Number
	//int Number = 1;

	//string
	//顯示的訊息
	static string Message_Input_String;

	//bool
	//是否使用動態顯示 true:使用 false:不使用
	static bool Message_Bool = false;

	//List
	//訊息List
	//List<string> Message_List = new List<string>();

	//宣告UI=======================================
	//Text
	//訊息Text
	public Text[]  Message_String_Text = new Text[]{};

	//Animation
	//訊息Animation
	public Animation[] Message_String_Animation = new Animation[]{};

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//使用動態顯示
		if (Message_Bool == true) {
			//使用動態顯示
			Show_Message_Function (Message_Input_String);
			//防止重複顯示
			Message_Bool = false;
			//顯示的訊息初始化
			Message_Input_String = "";
		}
	}


	//副程式:顯示訊息(string 要顯示的訊息字串)==============================================
	void Show_Message_Function(string Message){
		//如果未達顯示訊息上限
		if (String_Quantity < String_Max) {
			
			//放入Text
			Message_String_Text[String_Quantity].text = "" + Message;
			//顯示Text
			Message_String_Animation[String_Quantity].Play();
			//Message_String_Text[String_Quantity].color = new Color32(0,0,0,255);


			//設定訊息現在數量
			String_Quantity = String_Quantity + 1;
		}
		//已達顯示訊息上限
		else{
			/*//方法1(從頭開始顯示)*************************************
			//將新的訊息放入第1個Text
			Message_String_Text[0].text = "" + Message;
		
			//設定訊息現在數量為 1
			String_Quantity = 1;*/

			//方法2(將所有訊息向上放)*************************************
			//將所有訊息向上放
			for (int i = 1; i < String_Max; i++) {
				Message_String_Text [i-1].text = Message_String_Text [i].text;
			}

			//最後1個text變為最新訊息
			Message_String_Text[String_Max - 1].text = "" + Message;
			//顯示最後1個Text
			Message_String_Animation[String_Max - 1].Play();

		}

	}//Show_Message_Function

	//提供給需要使用動態顯示的其它腳本
	public static void Input_String(string Message_String){
		Message_Bool = true;
		Message_Input_String = Message_String;
	}
}
