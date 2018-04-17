<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Obras_general_upload.aspx.cs" Inherits="SICA_upload_Obras_general_upload" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="../js/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="../js/modernizr-2.5.3.js"></script>



    <script type="text/javascript">
        var selectedFiles;

        $(document).ready(function() {

            if (!Modernizr.draganddrop) {
                alert("This browser doesn't support File API and Drag & Drop features of HTML5!");
                return;
            }

            var box;
            box = document.getElementById("box");
            box.addEventListener("dragenter", OnDragEnter, false);
            box.addEventListener("dragover", OnDragOver, false);
            box.addEventListener("drop", OnDrop, false);

            $("#upload").click(function() {
                var data = new FormData();
                for (var i = 0; i < selectedFiles.length; i++) {
                    data.append(selectedFiles[i].name, selectedFiles[i]);
                }
                $.ajax({
                    type: "POST",
                    url: "FileHandler.ashx",
                    contentType: false,
                    processData: false,
                    data: data,
                    success: function(result) {
                        alert(result);
                    },
                    error: function() {
                        alert("There was error uploading files!");
                    }
                });
            });

        });

        function OnDragEnter(e) {
            e.stopPropagation();
            e.preventDefault();
        }

        function OnDragOver(e) {
            e.stopPropagation();
            e.preventDefault();
        }

        function OnDrop(e) {
            e.stopPropagation();
            e.preventDefault();
            selectedFiles = e.dataTransfer.files;
            $("#box").text(selectedFiles.length + " Fichero(s) Listos para subir");
        }
    </script>
    <style>
      #box {
        width:100px;
        height:100px;
        text-align:center;
        vertical-align:middle;
        border:2px solid #c4e1ff;
        background-color:#EEEEEE;
        padding:15px;
        font-family:Arial;
        font-size:16px;
        margin-top:35px;
      }

    </style>
    
</head>
<body>
    <form id="form1" runat="server">
        <center>
            <div id="box">Arrastre aquí los ficheros que desea adjuntar</div>
            <br />
            <input id="upload" type="button" value="Subir" />
        </center>
    </form>
</body>
</html>
