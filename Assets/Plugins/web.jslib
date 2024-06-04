var plugin = {

  /////MAIN/////
  GamePlatform : function()
    {
        console.log(location.hostname);
        p = UTF8ToString(location.hostname);
        myGameInstance.SendMessage('Init', 'ChangePlatform', p);
    },
  /////MAIN/////

  DownloadFile : function(array, size, fileNamePtr)
  {
    var fileName = UTF8ToString(fileNamePtr);
 
    var bytes = new Uint8Array(size);
    for (var i = 0; i < size; i++)
    {
       bytes[i] = HEAPU8[array + i];
    }
 
    var blob = new Blob([bytes]);
    var link = document.createElement('a');
    link.href = window.URL.createObjectURL(blob);
    link.download = fileName;
 
    var event = document.createEvent("MouseEvents");
    event.initMouseEvent("click");
    link.dispatchEvent(event);
    window.URL.revokeObjectURL(link.href);
  }, 


  /////YANDEX//////
    GameReady : function()
    {
        ysdk.features.LoadingAPI.ready();
    },

    IsMobile : function()
    {
        if (/iPhone|iPad|iPod|Android/i.test(navigator.userAgent)) {
            myGameInstance.SendMessage('Init', 'ItIsMobile');
        }
    },

    RateGame: function () {
        ysdk.feedback.canReview()
        .then(({ value, reason }) => {
            if (value) {
                ysdk.feedback.requestReview()
                    .then(({ feedbackSent }) => {
                        console.log(feedbackSent);
                    })
            } else {
                console.log(reason)
            }
        })
    },

    SaveExtern: function(date) {
        var dateString = UTF8ToString(date);
        var myobj = JSON.parse(dateString);
        player.setData(myobj);
      },

    LoadExtern: function(){
        player.getData().then(_date => {
            //const myJSON = JSON.stringify(_date);
            const myJSON = UTF8ToString(_date);
            myGameInstance.SendMessage('Init', 'SetPlayerData', myJSON);
        });
    },  	
    
    AdInterstitial : function () {
        ysdk.adv.showFullscreenAdv({
          callbacks: {
        onOpen: function(wasShown) {
          myGameInstance.SendMessage('Init', 'StopMusAndGame');
        },
        onClose: function(wasShown) {
          myGameInstance.SendMessage('Init', 'ResumeMusAndGame');
        },
        onError: function(error) {
          // some action on error
        }
        }
    });
    },

    AdReward : function(){
        ysdk.adv.showRewardedVideo({
        callbacks: {
        onOpen: () => {
          console.log('Video ad open.');
          myGameInstance.SendMessage('Init', 'StopMusAndGame');
        },
        onRewarded: () => {
          //myGameInstance.SendMessage('Init', 'OnRewarded');
        },
        onClose: () => {
          console.log('Video ad closed.');
          myGameInstance.SendMessage('Init', 'OnRewarded');
          myGameInstance.SendMessage('Init', 'ResumeMusAndGame');
        }, 
        onError: (e) => {
          console.log('Error while open video ad:', e);
        }
    }
    });
    },

    GetLang : function(){
      var lang = ysdk.environment.i18n.lang;
      var bufferSize = lengthBytesUTF8(lang) + 1;
      var buffer = _malloc(bufferSize);
      stringToUTF8(lang, buffer, bufferSize);
      return buffer;
    }, 

    SetToLeaderboard : function(value, leaderboardName){
          leaderboardName = UTF8ToString(leaderboardName);
          ysdk.getLeaderboards()
            .then(lb => {
          // Без extraData
          lb.setLeaderboardScore(leaderboardName, value);
          });
    },

    GetDomain: function() {
      var lang = ysdk.environment.i18n.tld;
      var bufferSize = lengthBytesUTF8(lang) + 1;
      var buffer = _malloc(bufferSize);
      stringToUTF8(lang, buffer, bufferSize);
      return buffer;
    },

    

    CheckPlayGame: function (id) {
       
        player.getIDsPerGame()
          .then(res => {
            console.log(res);
            console.log(res.length);

            for (let i = 0; i < res.length; i++)
            {
                console.log(res[i].appID);
                console.log(i);

                if(id == res[i].appID)
                {
                    console.log("true");
                    myGameInstance.SendMessage('Init', 'EnablePlayedGameToggle', id);
                    return "true";
                }
            }   
            myGameInstance.SendMessage('Init', 'DisablePlayedGameToggle', id);
          });

        return "false";
    },

    BuyItem : function (idOrTag, d) {
    	idOrTag = UTF8ToString(idOrTag);
      var dateString = UTF8ToString(d);
      var myobj = JSON.parse(dateString);
      player.setData(myobj);
    	ysdk.getPayments({ signed: true }).then(_payments => {
        	payments = _payments;
        	payments.purchase(idOrTag).then(purchase => {
        		myGameInstance.SendMessage('Init', 'OnPurchasedItem');
        		payments.consumePurchase(purchase.purchaseToken); //для разовых покупок
        		window.focus();
    		})
	    	}).catch(err => {
	        	console.alert(err);
	   	}).catch(err => {
	        console.alert(err);
	    })
    },
    CheckBuyItem: function (idOrTag) {
      	idOrTag = UTF8ToString(idOrTag);
        console.log(idOrTag);

        payments.getPurchases().then(purchases => purchases.forEach(consumePurchase));


  		  payments.getPurchases().then(purchases => {
  		    if (purchases.some(purchase => purchase.productID === idOrTag)) {
  		    	myGameInstance.SendMessage('Init', 'SetPurchasedItem');
  		    }
  		  }).catch(err => {
  		    // Выбрасывает исключение USER_NOT_AUTHORIZED для неавторизованных пользователей.
  		  })
	  },

    GetLeaderboard: function (type, number, name) {
      type = UTF8ToString(type);
      console.log(type);

      name = UTF8ToString(name);
      console.log(type);

          ysdk.getLeaderboards()
      .then(lb => {
        // Получение 10 топов
        lb.getLeaderboardEntries(name, { quantityTop: 10 })
          .then(res => {
            console.log(res);
            if (res.entries.length <= number)
            {
              myGameInstance.SendMessage('Init', 'EndGetLeaderboardsValue');
              console.log("NULL");
              return;
            }
            else if (type == "score")
            {
              console.log("SCORE");              
              console.log(String(res.entries[number].score));
              var message = String(res.entries[number].score) + "," + String(name);
              myGameInstance.SendMessage('Init', 'GetLeadersScore', message);
              //return String(res.entries[number].score);
            }
            else if (type == "name")
            {
              console.log("NAME");
              console.log(String(res.entries[number].player.publicName))
              var message = String(res.entries[number].player.publicName) + "," + String(name);
              myGameInstance.SendMessage('Init', 'GetLeadersName', message);
              //return UTF8ToString(res.entries[number].player.publicName);
            }
          });
      });
    },
  /////YANDEX//////
};

mergeInto(LibraryManager.library, plugin);