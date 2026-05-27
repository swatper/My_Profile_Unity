mergeInto(LibraryManager.library, {
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
    }
});