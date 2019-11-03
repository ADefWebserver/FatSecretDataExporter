window.jsFunctions = {
    saveFile: function (filename, byteArray, contentType) {
        if (window.navigator && window.navigator.msSaveOrOpenBlob) {
            window.navigator.msSaveOrOpenBlob(byteArray, filename);
        }
        else {
            let link = document.createElement('a');
            let blob = new Blob([byteArray], { type: contentType });

            link.href = "data:application/octet-stream;base64," + byteArray;
            link.target = "_blank";
            link.download = filename;

            document.body.appendChild(link);
            link.click();
            document.body.removeChild(link);
        }
    }
};