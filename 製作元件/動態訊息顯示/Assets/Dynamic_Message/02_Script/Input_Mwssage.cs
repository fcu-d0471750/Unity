//動態顯示訊息
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Input_Mwssage : MonoBehaviour {

	string ssss;
	float Timee;
	int a = 1;
	string Message_String_Image;

    private bool Power = false;
	
	// Update is called once per frame
	void Update () {
        if (Power == true)
        {
            Timee += Time.deltaTime;
            if (Timee > 1.5f)
            {
                /*//Message_Text測試
                ssss = "第" + a + "筆";
                Message_Text.Input_String (ssss);
                a = a + 1;
                Timee = 0.0f;*/

                //Message_Text_Image測試
                ssss = "第" + a + "筆";
                //圖片名稱
                Message_String_Image = "BreadStore_01";
                Message_Text_Image.Input_String_Image(ssss, Message_String_Image);
                a = a + 1;
                Timee = 0.0f;

            }//if
        }

	}//Update



    //副程式:啟動/關閉動態顯示訊息
    public void set_Power(bool pb)
    {
        Power = pb;
        if (pb == false) Timee = 0.0f;
    }

}
