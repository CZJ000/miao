using System.Collections.Generic;
using PureMVC.Patterns;
using PureMVC.Interfaces;
using Mono.Data.Sqlite;

public class NeighborInfoProxy : Proxy
{
    public new const string NAME = "NeighborInfoProxy";
    public int userID;
    public int userLV;
    private List<NeighborInfoVO> neighborList = new List<NeighborInfoVO>();
    private bool isFirstGet = true;
    public Dictionary<int, BuildModelVo> BuildModelVoDic = new Dictionary<int, BuildModelVo>();

    public NeighborInfoProxy() : base(NAME){ }

    public void Init()
    {
        DbAccess dbAccess = new DbAccess();

        //从info_ai表里面选取与该用户相差等级小于2的项
        string query = string.Format("SELECT * FROM info_aiuser ORDER BY ABS(lv - {1}) LIMIT 2", userID, userLV);
        SqliteDataReader reader = dbAccess.ExecuteQuery(query);
        while (reader.Read())
        {
            NeighborInfoVO infoVO = new NeighborInfoVO();
            infoVO.PlayerName = Utils.GetString(reader["playername"]);
            infoVO.Level = Utils.GetInt(reader["lv"]);
            infoVO.Gold = Utils.GetInt(reader["gold"]);
            infoVO.Diamond = Utils.GetInt(reader["diamond"]);
            infoVO.Exp = Utils.GetInt(reader["exp"]);
            infoVO.facility1 = Utils.GetInt(reader["facility1"]);
            infoVO.facility2 = Utils.GetInt(reader["facility2"]);
            infoVO.facility3 = Utils.GetInt(reader["facility3"]);
            infoVO.facility4 = Utils.GetInt(reader["facility4"]);
            neighborList.Add(infoVO);
        }
        dbAccess.CloseSqlConnection();
    }

    public void RefreshNeighborInfoValue()
    {
        if (isFirstGet)
        {
            Init();
            isFirstGet = false;
        }
    }

    private void setbuildmodedic()
    {
        if(neighborList.Count >= 1)
        {
            NeighborInfoVO Data = neighborList[0];
            for (int i = 5; i <= 8; i++)
            {
                if (!BuildModelVoDic.ContainsKey(i))
                {
                    BuildModelVo Vo = new BuildModelVo();
                    Vo.ModeltrsId = i;
                    switch (i)
                    {
                        case 5:
                            Vo.Modelid = Data.facility1;
                            break;
                        case 6:
                            Vo.Modelid = Data.facility2;
                            break;
                        case 7:
                            Vo.Modelid = Data.facility3;
                            break;
                        case 8:
                            Vo.Modelid = Data.facility4;
                            break;
                    }
                    BuildModelVoDic.Add(i, Vo);
                }
                else
                {
                    switch (i)
                    {
                        case 5:
                            BuildModelVoDic[i].Modelid = Data.facility1;
                            break;
                        case 6:
                            BuildModelVoDic[i].Modelid = Data.facility2;
                            break;
                        case 7:
                            BuildModelVoDic[i].Modelid = Data.facility3;
                            break;
                        case 8:
                            BuildModelVoDic[i].Modelid = Data.facility4;
                            break;
                    }
                }
            }
        }
        if(neighborList.Count > 1)
        {
            NeighborInfoVO Data = neighborList[1];
            for (int i = 9; i <= 12; i++)
            {
                if (!BuildModelVoDic.ContainsKey(i))
                {
                    BuildModelVo Vo = new BuildModelVo();
                    Vo.ModeltrsId = i;
                    switch (i)
                    {
                        case 9:
                            Vo.Modelid = Data.facility1;
                            break;
                        case 10:
                            Vo.Modelid = Data.facility2;
                            break;
                        case 11:
                            Vo.Modelid = Data.facility3;
                            break;
                        case 12:
                            Vo.Modelid = Data.facility4;
                            break;
                    }
                    BuildModelVoDic.Add(i, Vo);
                }
                else
                {
                    switch (i)
                    {
                        case 9:
                            BuildModelVoDic[i].Modelid = Data.facility1;
                            break;
                        case 10:
                            BuildModelVoDic[i].Modelid = Data.facility2;
                            break;
                        case 11:
                            BuildModelVoDic[i].Modelid = Data.facility3;
                            break;
                        case 12:
                            BuildModelVoDic[i].Modelid = Data.facility4;
                            break;
                    }
                }
            }
        }

    }

    public void sendinitmodeldata()
    {
        setbuildmodedic();
        SendNotification(BuildingModelMediator.INITBUILTMODEL, BuildModelVoDic);
    }

    public int GetNeighbor1Facility(int id)
    {
        
        if (neighborList.Count >= 1)
        {
            NeighborInfoVO Data = neighborList[0];
            switch (id)
            {
                case 1: return Data.facility1;
                case 2: return Data.facility2;
                case 3: return Data.facility3;
                case 4: return Data.facility4;
            }
        }

        return 0;
    }

    public int GetNeighbor2Facility(int id)
    {
        if (neighborList.Count > 1)
        {
            NeighborInfoVO Data = neighborList[1];
            switch (id)
            {
                case 1: return Data.facility1;
                case 2: return Data.facility2;
                case 3: return Data.facility3;
                case 4: return Data.facility4;
            }
        }
        return 0;
    }
}
