using System.Collections.Generic;
public class stat_buildingRow
{
    public int id;
    public int type;
    public string name;
    public string description;
    public int needlv;
    public int series;
    public int customervol;
    public int buildingskill;
    public string recruittype;
    public string description2;
}
/// <summary>
/// Auto Generated By ZJRTool, Do Not Modify
/// </summary>
public class stat_building : DatabaseSerializer
{
    private static stat_building mInstance;
    public List<stat_buildingRow> rowList = new List<stat_buildingRow>();
    public static stat_building GetInstance()
    {
        return mInstance ?? (mInstance = new stat_building());
    }
    protected override void AddRow(object[] rowInfo)
    {
        stat_buildingRow row = new stat_buildingRow();
        row.id = GetInt(rowInfo[0]);
        row.type = GetInt(rowInfo[1]);
        row.name = GetString(rowInfo[2]);
        row.description = GetString(rowInfo[3]);
        row.needlv = GetInt(rowInfo[4]);
        row.series = GetInt(rowInfo[5]);
        row.customervol = GetInt(rowInfo[6]);
        row.buildingskill = GetInt(rowInfo[7]);
        row.recruittype = GetString(rowInfo[8]);
        row.description2 = GetString(rowInfo[9]);
        rowList.Add(row);
    }
}
