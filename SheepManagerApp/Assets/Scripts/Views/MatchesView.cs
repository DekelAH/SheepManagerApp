using Assets.Scripts.DTO;
using Assets.Scripts.Infrastructure;
using Assets.Scripts.Infrastructure.Providers;
using System.Collections.Generic;
using UnityEngine;

public class MatchesView : MonoBehaviour
{
    #region Editor

    [SerializeField]
    private MatchDataContainer _matchDataContainerPrefab;

    [SerializeField]
    private RectTransform _panelRectTransform;

    #endregion

    #region Methods

    private void Start()
    {
        CreateMatchesContainers(GetMatches());
    }

    private List<MatchResponse> GetMatches()
    {
        var matches = ApplicationDataManager.Herd.matches;
        return matches;
    }

    private void CreateMatchesContainers(List<MatchResponse> matches)
    {
        foreach (var matchResponse in matches)
        {
            var button = Instantiate<MatchDataContainer>(_matchDataContainerPrefab, _panelRectTransform);
            button.SetTagNumbers(matchResponse.femaleTagNumber, matchResponse.maleTagNumber);
        }
    }

    public void OnBackToMainBtnClick()
    {
        SectionHandler.LoadSection(SceneNameProvider.GetMainMenuName);
    }

    #endregion
}
