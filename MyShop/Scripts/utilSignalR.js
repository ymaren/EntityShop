$(function () {

    $('#chatBody').hide();
    $('#loginBlock').show();
    // Ссылка на автоматически-сгенерированный прокси хаба
    //$.connection.chatHub
    var chat = $.connection.chatHub;
    //alert("kjkjj");
    //var currentMember = '@Html.Raw(@ViewBag.Name)';
    //alert(currentMember);
    //$("#txtUserName").val("321");
    // Объявление функции, которая хаб вызывает при получении сообщений
    chat.client.addMessage = function (name, message) {

        var currentName = $("#txtUserName").val();
        if (currentName==name) {
         $('#chatroom').append('<p style="text-align: right;">' + htmlEncode(message) + '</p>');
            //alert("same");
        }
        else {
            $('#chatroom').append('<p style="text-align: left;"><b>' + htmlEncode(name) +'</b></br> ' + htmlEncode(message) + '</p>');
            //alert("different");
        }
        document.getElementById("chatroomHeader").scrollTop = document.getElementById("chatroom").scrollHeight;

        // Добавление сообщений на веб-страницу 
       
        //document.getElementById("chatroom").value = document.getElementById("chatroom").value+"\n"+ ( htmlEncode(name) +":"  + htmlEncode(message) );
    };

    // Функция, вызываемая при подключении нового пользователя
    chat.client.onConnected = function (id, userName, allUsers) {

        $('#loginBlock').hide();
        $('#chatBody').show();
        // установка в скрытых полях имени и id текущего пользователя
        $('#hdId').val(id);
        $('#username').val(userName);
        $('#header').html('Welcome, ' + userName);

        // Добавление всех пользователей
        for (i = 0; i < allUsers.length; i++) {

            AddUser(allUsers[i].ConnectionId, allUsers[i].Name);
        }
    }

    // Добавляем нового пользователя
    chat.client.onNewUserConnected = function (id, name) {

        AddUser(id, name);
    }

    // Удаляем пользователя
    chat.client.onUserDisconnected = function (id, userName) {

        $('#' + id).remove();
    }

    // Открываем соединение
    $.connection.hub.start().done(function () {

        $('#sendmessage').click(function () {
            // Вызываем у хаба метод Send
            chat.server.send($('#username').val(), $('#message').val());
            $('#message').val('').focus();
        });

        // обработка логина
        $("#btnLogin").click(function () {

            var name = $("#txtUserName").val();
            if (name.length > 0) {

                chat.server.connect(name);
            }
            else {
                alert("Введите имя");

                //chat.server.("Ann")
            }
        });
    });
});
// Кодирование тегов
function htmlEncode(value) {
    var encodedValue = $('<div />').text(value).html();
    return encodedValue;
}
//Добавление нового пользователя
function AddUser(id, name) {

    var userId = $('#hdId').val();

    if (userId != id) {

        $("#chatusers").append('<p id="' + id + '"><b>' + name + '</b></p>');
    }
}

function Stey(name) {

    var userId = $('#hdId').val();

    if (userId != id) {

        $("#chatusers").append('<p id="' + id + '"><b>' + name + '</b></p>');
    }
}