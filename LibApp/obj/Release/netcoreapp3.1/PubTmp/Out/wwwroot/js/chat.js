"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chat").build();

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

function getDate() {
    var today = new Date().toISOString().slice(0, 10);
    today += ('T' + Date.prototype.getHours().toString() + ':' + Date.prototype.getMinutes().toString() + Date.prototype.getMinutes().toString());
    return today;
}

connection.on("ReceiveMessage", function (user, message) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");

    var divMsg = document.createElement("div");
    divMsg.setAttribute('class', 'message');
    var header = document.createElement('header');
    header.innerHTML = user;
    var p = document.createElement('p');
    p.innerHTML = msg;
    var footer = document.createElement('footer');
    footer.innerHTML = new Date().toISOString().slice(11, 16);;

    divMsg.appendChild(header);
    divMsg.appendChild(p);
    divMsg.appendChild(footer);

    document.getElementById("messagesList").appendChild(divMsg);
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").innerHTML;
    var message = document.getElementById("messageInput").value;
    if (user == "" || message == "") {
        toastr.error("Fill in the Message field");
        return;
    }
    connection.invoke("SendMessage", user, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();

    var date;
    date = new Date().toISOString().slice(0, 19);

    var data = {
        text: message,
        sent: date,
        appUser: {
            userName: user
        }
    };

    data = JSON.stringify(data);

    var response = fetch('/api/messages', {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: data
    }).then(response => response.json()).then(console.log(response));

    document.getElementById('messageInput').value = "";

    document.getElementById("sendButton").disabled = true;
    setTimeout(function () {
        document.getElementById("sendButton").disabled = false;
    }, 10000);

});