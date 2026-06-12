mergeInto(LibraryManager.library, {
    // 클립보드에 텍스트 복사 함수
    CopyTextToClipboardWebGL: function (textPtr) {
        // 유니티 포인터에서 문자열 변환
        var text = UTF8ToString(textPtr);

        // 최신 브라우저의 클립보드 API 호출
        if (navigator.clipboard && navigator.clipboard.writeText) {
            navigator.clipboard.writeText(text).then(function() {
                console.log("WebGL Clipboard Copy Success!");
            }).catch(function(err) {
                console.error("WebGL Clipboard Copy Failed: ", err);
            });
        } else {
            // 구형 브라우저 대응용 fallback (우회책)
            var textArea = document.createElement("textarea");
            textArea.value = text;
            textArea.style.position = "fixed";  // 화면 밖으로 치우기
            document.body.appendChild(textArea);
            textArea.focus();
            textArea.select();
            try {
                document.execCommand('copy');
                console.log("WebGL Clipboard Copy Success (Fallback)!");
            } catch (err) {
                console.error("WebGL Clipboard Copy Failed (Fallback): ", err);
            }
            document.body.removeChild(textArea);
        }
    },

    //전체 화면 전환 함수
    SetFullscreenWebGL: function (isFullscreen) {
        if (isFullscreen) {
            // 전체 화면으로 가려고 할 때
            if (!document.fullscreenElement) {
                var docEl = document.documentElement;
                (docEl.requestFullscreen || docEl.webkitRequestFullscreen || docEl.msRequestFullscreen).call(docEl);
            }
        } else {
            // 전체 화면에서 나오려고 할 때
            // [중요] 현재 전체 화면 상태일 때만 exit을 호출하도록 검사 추가!
            if (document.fullscreenElement || document.webkitFullscreenElement || document.msFullscreenElement) {
                if (document.exitFullscreen) {
                    document.exitFullscreen();
                } else if (document.webkitExitFullscreen) {
                    document.webkitExitFullscreen();
                } else if (document.msExitFullscreen) {
                    document.msExitFullscreen();
                }
            }
        }
    },

    //전체화면 Listener 등록 및 동기화 함수
    RegisterFullscreenListener: function () {
        //전체 화면 상태 변경 이벤트 리스너 구독 및 감지
        document.addEventListener("fullscreenchange", function () {
            var isNow = !!document.fullscreenElement;
            SendMessage('FullScreenButton', 'SyncFullScreen', isNow ? 1 : 0);
        });

        //초기 상태 동기화
        var isInitial = !!document.fullscreenElement;
        SendMessage('FullScreenButton', 'SyncFullScreen', isInitial ? 1 : 0);
    }
});