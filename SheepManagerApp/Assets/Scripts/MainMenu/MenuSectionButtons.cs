using UnityEngine;

public class MenuSectionButtons : MonoBehaviour
{
    #region Editor

    [SerializeField]
    private int _transferToSection;

    #endregion

    #region Methods

    public void OnSectionBtnClick(string sectionName)
    {
        SectionHandler.LoadSection(sectionName);
    }

    #endregion

}
