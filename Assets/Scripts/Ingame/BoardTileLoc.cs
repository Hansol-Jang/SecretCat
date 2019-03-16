using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardTileLoc : MonoBehaviour {

    public int floorNumber; //층 갯수
    public int three_star_point; //3성 점수 하한
    public int two_star_point; //2성 점수 하한
    public int bone_limit; //주어진 뼈 갯수
    public int[] player_loc = new int[3]; //0 : 고양이의 X축 위치, 1: 고양이의 Y축 위치, 2: 고양이의 층 위치
    public int[] cleartile_loc = new int[3]; //0: X축, 1: Y축, 2: 층 위치
    public int[,] dog_num;
    public int[,] portal_num;
    public int[,] stair_num;

    public void Setting()
    {
        int level = GameManager.instance.level;

        floorNumber = (int)GameManager.instance.boardInfo[0]["Board"+level];
        three_star_point = (int)GameManager.instance.boardInfo[1]["Board" + level];
        two_star_point = (int)GameManager.instance.boardInfo[2]["Board" + level];
        bone_limit = (int)GameManager.instance.boardInfo[3]["Board" + level];
        player_loc = new int[(int)GameManager.instance.boardInfo[4]["Board" + level]];
        player_loc[0] = (int)GameManager.instance.boardInfo[5]["Board" + level];
        player_loc[1] = (int)GameManager.instance.boardInfo[6]["Board" + level];
        player_loc[2] = (int)GameManager.instance.boardInfo[7]["Board" + level];
        cleartile_loc = new int[(int)GameManager.instance.boardInfo[8]["Board" + level]];
        cleartile_loc[0] = (int)GameManager.instance.boardInfo[9]["Board" + level];
        cleartile_loc[1] = (int)GameManager.instance.boardInfo[10]["Board" + level];
        cleartile_loc[2] = (int)GameManager.instance.boardInfo[11]["Board" + level];

        int temp_num = int.Parse(GameManager.instance.boardObject[1]["Board" + level].ToString());
        dog_num = new int[int.Parse(GameManager.instance.boardObject[0]["Board" + level].ToString()), temp_num];
        if (dog_num.GetLength(0) > 0)
        {
            for (int i = 0; i < dog_num.GetLength(0); i++)
            {
                dog_num[i, 0] = int.Parse(GameManager.instance.boardObject[2 + i * temp_num]["Board" + level].ToString());
                int temp_dir = 0;
                switch (GameManager.instance.boardObject[3 + i * temp_num]["Board" + level].ToString())
                {
                    case "좌":
                        temp_dir = 0;
                        break;
                    case "상":
                        temp_dir = 1;
                        break;
                    case "우":
                        temp_dir = 2;
                        break;
                    case "하":
                        temp_dir = 3;
                        break;
                }
                dog_num[i, 1] = temp_dir;
                dog_num[i, 2] = int.Parse(GameManager.instance.boardObject[4 + i * temp_num]["Board" + level].ToString());
                dog_num[i, 3] = int.Parse(GameManager.instance.boardObject[5 + i * temp_num]["Board" + level].ToString());
                dog_num[i, 4] = int.Parse(GameManager.instance.boardObject[6 + i * temp_num]["Board" + level].ToString());
            }
        }

        temp_num = int.Parse(GameManager.instance.boardPortal[1]["Board" + level].ToString());
        portal_num = new int[int.Parse(GameManager.instance.boardPortal[0]["Board" + level].ToString()), temp_num];
        if (portal_num.GetLength(0) > 0)
        {
            for (int i = 0; i < portal_num.GetLength(0); i++)
            {
                portal_num[i,0] = int.Parse(GameManager.instance.boardPortal[2 + i * temp_num]["Board" + level].ToString());
                portal_num[i, 1] = int.Parse(GameManager.instance.boardPortal[3 + i * temp_num]["Board" + level].ToString());
                portal_num[i, 2] = int.Parse(GameManager.instance.boardPortal[4 + i * temp_num]["Board" + level].ToString());
                portal_num[i, 3] = int.Parse(GameManager.instance.boardPortal[5 + i * temp_num]["Board" + level].ToString());
            }
        }

        temp_num = int.Parse(GameManager.instance.boardStair[1]["Board" + level].ToString());
        stair_num = new int[int.Parse(GameManager.instance.boardStair[0]["Board" + level].ToString()), temp_num];
        if (stair_num.GetLength(0) > 0)
        {
            for (int i = 0; i < stair_num.GetLength(0); i++)
            {
                stair_num[i, 0] = int.Parse(GameManager.instance.boardStair[2 + i * temp_num]["Board" + level].ToString());
                int temp_dir = 0;
                switch (GameManager.instance.boardStair[3 + i * temp_num]["Board" + level].ToString())
                {
                    case "위":
                        temp_dir = 1;
                        break;
                    case "아래":
                        temp_dir = -1;
                        break;
                }
                stair_num[i, 1] = temp_dir;
                stair_num[i, 2] = int.Parse(GameManager.instance.boardStair[4 + i * temp_num]["Board" + level].ToString());
                stair_num[i, 3] = int.Parse(GameManager.instance.boardStair[5 + i * temp_num]["Board" + level].ToString());
                stair_num[i, 4] = int.Parse(GameManager.instance.boardStair[6 + i * temp_num]["Board" + level].ToString());
            }
        }
    }
}