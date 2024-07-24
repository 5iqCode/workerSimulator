mergeInto(LibraryManager.library, {

  SaveExtern: function (date) {
    var dateString = UTF8ToString(date);
    var myObj = JSON.parse(dateString);
    player.setData(myObj);
  },

  LoadExtern: function () {
    if(player!=null){
          player.getData().then(_date=>{
      const myJSON = JSON.stringify(_date);
      myGameInstance.SendMessage('LoadedInfo','SetPlayerInfo',myJSON);


      const languageName = JSON.stringify(language);

      myGameInstance.SendMessage('LoadedInfo','SetDefaultLanguage',languageName);

      if(isDesktop==true){
        myGameInstance.SendMessage('LoadedInfo','SetDevice',1);
      } else{
        myGameInstance.SendMessage('LoadedInfo','SetDevice',0);
      }
      
    })
        }
  },

InitPlayerExtern: function (_currentLvl) {
    function initPlayer() {
    return ysdk.getPlayer().then(_player => {
            player = _player;

            return player;
        });
}
    function SetLeaderBordResult() {
      ysdk.getLeaderboards()
    .then(lb => {
      // Без extraData
      leaderboard = lb;
        lb.setLeaderboardScore('currentLvl', _currentLvl);

        GetLeaderboardScores();
      });
    }

initPlayer().then(_player => {
        if (_player.getMode() === 'lite') {
            // Игрок не авторизован.
            ysdk.auth.openAuthDialog().then(() => {
                      SetLeaderBordResult() ;
                    initPlayer().catch(err => {
                        myGameInstance.SendMessage('LoadedInfo','ErrorLoad',1);
                    });
                }).catch(() => {
                    myGameInstance.SendMessage('LoadedInfo','ErrorLoad',1);
                });
        } else
          SetLeaderBordResult() ;
        {

        }
    }).catch(err => {
        myGameInstance.SendMessage('LoadedInfo','ErrorLoad',1);
    });
  },

  ReloadGameExtern: function () {
    location.reload();
  },

  ShowAdv: function () {
const _stateSound = true;

    ysdk.adv.showFullscreenAdv({
    callbacks: {
        onOpen: function(wasOpen) {
          myGameInstance.SendMessage('LoadedInfo', 'SetFocus', 0);
        },
        onClose: function(wasShown) {
          myGameInstance.SendMessage('LoadedInfo', 'UnloadScenePause', 1);
        },
        onError: function(error) {
          myGameInstance.SendMessage('LoadedInfo', 'UnloadScenePause', 1);
        }
    }
  })

  },
  RewardedVideoExtern: function () {

      ysdk.adv.showRewardedVideo({
    callbacks: {
        onOpen: () => {
          console.log('Video ad open.');
        },
        onRewarded: () => {
          myGameInstance.SendMessage('LoadedInfo','RewardedTrue',1);
        },
        onClose: () => {
          myGameInstance.SendMessage('LoadedInfo','PauseUnpauseGame',1);
        }, 
        onError: (e) => {
          myGameInstance.SendMessage('LoadedInfo','RewardedError',1);
        }
    }
})
  },

});