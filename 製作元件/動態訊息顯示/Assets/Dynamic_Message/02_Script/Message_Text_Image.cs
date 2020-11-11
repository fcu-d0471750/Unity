//動態顯示訊息、圖片
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic; //使用List

public class Message_Text_Image : MonoBehaviour {

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
	//Image
	//顯示的圖片名稱
	static string Message_Input_Image;

	//bool
	//是否使用動態顯示 true:使用 false:不使用
	static bool Message_Bool = false;

	//宣告UI=======================================
	//Text
	//訊息Text
	public Text[]  Message_String_Text = new Text[]{};
	//Image
	//訊息Image
	public Image[] Message_String_Image = new Image[]{}; 

	//Animation
	//訊息Animation
	public Animation[] Message_String_Image_Animation = new Animation[]{};


	// Update is called once per frame
	void Update () {
		//使用動態顯示
		if (Message_Bool == true) {
			//使用動態顯示
			Show_Message_Function (Message_Input_String , Message_Input_Image);
			//防止重複顯示
			Message_Bool = false;
			//顯示的訊息初始化
			Message_Input_String = "";
			//顯示的圖片初始化
			Message_Input_Image = null;
		}
	}


	//副程式:顯示訊息(string 要顯示的訊息字串 , 要顯示的圖片)==============================================
	void Show_Message_Function(string Message , string Message_Image){
		//如果未達顯示訊息上限
		if (String_Quantity < String_Max) {

			//放入Text
			Message_String_Text[String_Quantity].text = "" + Message;

			//放入Image
			Message_String_Image[String_Quantity].sprite = (Sprite)Resources.Load<Sprite> (Message_Image);

			//顯示訊息
			Message_String_Image_Animation[String_Quantity].Play();

			//設定訊息現在數量
			String_Quantity = String_Quantity + 1;
		}
		//以達顯示訊息上限
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
				Message_String_Image [i - 1] = Message_String_Image [i];
			}

			//最後1個text變為最新訊息
			Message_String_Text[String_Max - 1].text = "" + Message;
			//最後1個Image變為最新訊息
			Message_String_Image[String_Max - 1].sprite = (Sprite)Resources.Load<Sprite> (Message_Image);

			//顯示最後1個Text
			Message_String_Image_Animation[String_Max - 1].Play();

		}

	}//Show_Message_Function


	/*//測試Message
	public void Button_Message(){
		
		string S;

		S = "第" + Number.ToString() + "筆";
		Show_Message_Function (S);

		Number = Number + 1;
	
	}//Button_Message*/

	//提供給需要使用動態顯示的其它腳本
	public static void Input_String_Image(string Message_String , string Message_Image){
		Message_Bool = true;
		Message_Input_String = Message_String;
		Message_Input_Image = Message_Image;
	}
}
