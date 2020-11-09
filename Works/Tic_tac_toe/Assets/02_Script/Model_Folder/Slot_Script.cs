﻿/*
 *物件: Slot Script
 *
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot_Script : MonoBehaviour
{
    //======================================
    //宣告變數
    //======================================

    //Tic_tac_toe整體相關變數，九個slot統一使用，外部的Script下的Tic_tac_toe_Script上的Tic_tac_toe_Script
    //public Tic_tac_toe_Script Tic;

    //Sounds_Script
    //public Sounds_Script sounds_Script;

    //Model_Script
    public Model_Script model;

    //是否可以改變Sprite，代表是否被使用，true:可改變 false:不可改變。
    private bool Slotischange = true;

    //======================================
    //宣告UI
    //======================================
    //sprite:圈
    public Sprite Circle_Sprite;

    //sprite:叉
    public Sprite Cancel_Sprite;

    //抓取Slot底下的Sprite
    private SpriteRenderer render;

    //連線動畫(水平、垂直、右斜線、左斜線)
    public Animation[] LineAnimation;



    private void Awake()
    {    
        //綁定Slot下一層子物件的sprite
        render = transform.GetChild(0).GetComponent<SpriteRenderer>();  
    }

    //======================================
    //按下滑鼠左鍵
    //======================================
    private void OnMouseDown()
    {
        //如果Slot已經被使用，則不執行後面的所有指令。
        if (Slotischange == false) return;

        //抓取自身下一層子物件的sprite，設定為圈或叉。
        if(model.Tic.Get_Tic_Sprite() == true)render.sprite = Circle_Sprite;
        else render.sprite = Cancel_Sprite;

        //如果要直接抓下一層的子物件，則可以使用以下這行。
        //GetComponentInChildren<SpriteRenderer>().sprite = Circle_Sprite;


        //以slot自身為中心，向左右兩邊延伸Boxcoller各5單位(水平 -)
        Collider[] hits_H = Physics.OverlapBox(transform.position, new Vector3(5, 0, 1));

        //以slot自身為中心，向上下兩邊延伸Boxcoller各5單位(垂直 |)
        Collider[] hits_V = Physics.OverlapBox(transform.position, new Vector3(0, 5, 1));

        //以slot自身為中心，向右上左下兩邊延伸Boxcoller各5單位(右斜線 /)，(Quaternion是由來將Boxcoller旋轉角度用)
        Collider[] hits_SR = Physics.OverlapBox(transform.position, new Vector3(5, 0, 1) , new Quaternion(0,0,0.35f,0.9f));

        //以slot自身為中心，向右下左上兩邊延伸Boxcoller各5單位(左斜線 \)
        Collider[] hits_SL = Physics.OverlapBox(transform.position, new Vector3(5, 0, 1) , new Quaternion(0, 0, -0.35f, 0.9f));

        //用於是否播放連線動畫，共有4個動畫，預設為播放，true:播放 false:不播放
        bool[] LineAnimationBool = new bool[] { true , true , true , true };

        //迴圈用
        int i,j;

        
        //======================================
        //(水平 -)判斷自身和左右兩邊的空格，共3個格子，是否為相同圖型。
        //======================================
        for ( i = 0; i < model.Tic.Get_Line_Number(); i++) {
            //如果相同
            if (hits_H[i].transform.GetComponentInChildren<SpriteRenderer>().sprite == render.sprite) {
               
            }
            //如果不相同，則不播放連線動畫。
            else{
                LineAnimationBool[0] = false;
            }

        }//for
        

        
        //======================================
        //(垂直 |)判斷自身和上下兩邊的空格，共3個格子，是否為相同圖型。
        //======================================
        for ( i = 0; i < model.Tic.Get_Line_Number(); i++)
        {
            //如果相同
            if (hits_V[i].transform.GetComponentInChildren<SpriteRenderer>().sprite == render.sprite){
                
            }
            //如果不相同，則不播放連線動畫。
            else{
                LineAnimationBool[1] = false;
            }

        }//for
        

        

        //======================================
        //(右斜線 /)判斷右上左下兩邊的空格，共3個格子，是否為相同圖型。
        //======================================
        //因為斜線可能會有只有兩格的情況發生，所以要確定Slot自身兩邊的是有兩個格子的。
        if (hits_SR.Length < model.Tic.Get_Line_Number())
        {
            LineAnimationBool[2] = false;
        }
        else
        {
            for ( i = 0; i < model.Tic.Get_Line_Number(); i++){
                //如果相同
                if (hits_SR[i].transform.GetComponentInChildren<SpriteRenderer>().sprite == render.sprite){
                   
                }
                //如果不相同，則不播放連線動畫。
                else{
                    LineAnimationBool[2] = false;
                }

            }//for
            

           
        }

        //======================================
        //(左斜線 \)判斷右下左上兩邊的空格，共3個格子，是否為相同圖型。
        //======================================
        //因為斜線可能會有只有兩格的情況發生，所以要確定Slot自身兩邊的是有兩個格子的。
        if (hits_SL.Length < model.Tic.Get_Line_Number())
        {
            LineAnimationBool[3] = false;
        }
        else
        {
            for ( i = 0; i < model.Tic.Get_Line_Number(); i++)
            {
                //如果相同
                if (hits_SL[i].transform.GetComponentInChildren<SpriteRenderer>().sprite == render.sprite){
                    
                }
                //如果不相同，則不播放連線動畫。
                else{
                    LineAnimationBool[3] = false;
                }

            }//for
            
           
        }

        //======================================
        //播放連線動畫
        //======================================
        for ( j = 0; j < LineAnimationBool.Length; j++) {  
            //如果是可以播放動畫 且 此連線動畫不是null(因為有些Slot可能只有水平連線 或 垂直連線 或 左斜線 或 右斜線)
            if (LineAnimationBool[j] == true && LineAnimation[j]!=null) LineAnimation[j].Play();
        }


        /*
        //======================================
        //播放按下Slot的音效
        //======================================
        sounds_Script.Play_Slot_Push();

        //======================================
        //回合結束，變更圈或叉
        //======================================
        Tic.Tic_Change();
        */

        //玩家已經決定Slot的Sprite，Slot設為不能改變
        Slotischange = false;

        //呼叫Control改變UI
        model.Model_To_Control_Tic_Change(model.Tic.Get_Tic_Sprite());

    }//OnMouseDown






    //============================================================================================================
    //外部使用
    //============================================================================================================

    //======================
    //Getter
    //======================

    //Slotischange
    public bool Get_Slotischange() {
        return Slotischange;
    }



    //============================================================================================================
    //============================================================================================================
    //以下為將上面的連線判斷改為副程式，可以使用以下副程式 或 使用上面，都能達到相同功能。
    //============================================================================================================
    //============================================================================================================

    //======================================
    //判斷水平 -
    //======================================
    private bool Line_H(Collider[] hits_H) {

        //迴圈用
        int i;

        //======================================
        //(水平 -)判斷自身和左右兩邊的空格，共3個格子，是否為相同圖型。
        //======================================
        for (i = 0; i < model.Tic.Get_Line_Number(); i++)
        {
            //如果相同
            if (hits_H[i].transform.GetComponentInChildren<SpriteRenderer>().sprite == render.sprite){

            }
            //如果不相同，則不播放連線動畫，回傳false。
            else{
                return false;
            }

        }//for

        //完成水平連線，回傳true。
        return true;

    }

    //======================================
    //判斷垂直 -
    //======================================
    private bool Line_V(Collider[] hits_V)
    {

        //迴圈用
        int i;

        //======================================
        //(垂直 |)判斷自身和上下兩邊的空格，共3個格子，是否為相同圖型。
        //======================================
        for (i = 0; i < model.Tic.Get_Line_Number(); i++)
        {
            //如果相同
            if (hits_V[i].transform.GetComponentInChildren<SpriteRenderer>().sprite == render.sprite){
            }
            //如果不相同，則不播放連線動畫，回傳false。
            else{
                return false;
            }

        }//for

        //完成垂直連線，回傳true。
        return true;

    }

    //======================================
    //判斷右斜線 /
    //======================================
    private bool Line_SR(Collider[] hits_SR)
    {

        //迴圈用
        int i;

        //======================================
        //(右斜線 /)判斷右上左下兩邊的空格，共3個格子，是否為相同圖型。
        //======================================
        for (i = 0; i < model.Tic.Get_Line_Number(); i++)
        {
            //如果相同
            if (hits_SR[i].transform.GetComponentInChildren<SpriteRenderer>().sprite == render.sprite) {
            }
            //如果不相同，則不播放連線動畫，回傳false。
            else{
                return false;
            }

        }//for

        //完成右斜線連線，回傳true。
        return true;

    }

    //======================================
    //判斷左斜線 \
    //======================================
    private bool Line_SL(Collider[] hits_SL)
    {

        //迴圈用
        int i;

        //======================================
        //(左斜線 \)判斷右下左上兩邊的空格，共3個格子，是否為相同圖型。
        //======================================
        for (i = 0; i < model.Tic.Get_Line_Number(); i++)
        {
            //如果相同
            if (hits_SL[i].transform.GetComponentInChildren<SpriteRenderer>().sprite == render.sprite)
            {

            }
            //如果不相同，則不播放連線動畫，回傳false。
            else
            {
                return false;
            }

        }//for

        //完成左斜線連線，回傳true。
        return true;

    }


}//Slot_Script
