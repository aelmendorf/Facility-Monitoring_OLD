const gasBayConnection = new signalR.HubConnectionBuilder()
    .withUrl("http://172.20.4.209:443/hubs/gasbay")
    //.withUrl("http://localhost:5000/hubs/gasbay")
    .configureLogging(signalR.LogLevel.Information)
    .build();

var gasBayStore = new DevExpress.data.CustomStore({
    load: async function () {
        return await GetData();
    }, key: "Identifier"
});

const boxReadingData = [{
    "Identifier": "GasBay"
}];

async function startGasBay() {
    try {
        await gasBayConnection.start();
        console.log("Connected!");
        await setupPage();
    } catch (err) {
        console.log(err);
        setTimeout(async () => await startGasBay(), 5000);
    }
}

async function GetData() {
    var dto = await gasBayConnection.invoke("GetDeviceTable");
    for (var x = 0; x < dto.columns.length; x++) {
        boxReadingData.map((d) => d[dto.columns[x].header] = dto.row[dto.columns[x].columnName]);
    }
    return boxReadingData;
}

async function buildGasBayTable() { 
    generateData(dto);
    $("#boxReadings").dxDataGrid({
        dataSource: gasBayStore,
        visible: true
    });
}

async function switchMaint(data) {
    await gasBayConnection.invoke("SetMaint", data.value);
}

async function setupPage() {
    $("#boxReadings").dxDataGrid({
        dataSource: gasBayStore,
        visible: true
    });

    $("#maintSwitch").dxSwitch({
        onValueChanged: async function (e) {
            var success = await gasBayConnection.invoke("SetMaint", e.value);
            const li = document.createElement("li");
            if (success) {
                if (e.previousValue) {
                    li.textContent = "Switched Maintenance Off";
                } else {
                    li.textContent = "Switched Maintenance On";
                }
            } else {
                li.textContent = "Error Switching Maintenance";
            }
            document.getElementById("messagesList").appendChild(li);
        }
    });

    $("#alarmSwitch").dxSwitch({
        onValueChanged: async function (e) {
            var success = await gasBayConnection.invoke("SetAlarmDebug", e.value);
            const li = document.createElement("li");
            if (success) {
                if (e.previousValue) {
                    li.textContent = "Switched Alarm Off";
                } else {
                    li.textContent = "Switched Alarm On";
                }
            } else {
                li.textContent = "Error Switching Alarm";
            }
            document.getElementById("messagesList").appendChild(li);
        }
    });

    $("#warningSwitch").dxSwitch({
        onValueChanged: async function (e) {
            var success = await gasBayConnection.invoke("SetWarnDebug", e.value);
            const li = document.createElement("li");
            if (success) {
                if (e.previousValue) {
                    li.textContent = "Switched Warning Off";
                } else {
                    li.textContent = "Switched Warning On";
                }
            } else {
                li.textContent = "Error Switching Warning";
            }
            document.getElementById("messagesList").appendChild(li);
        }
    });
}

function OnConnected() {
    const li = document.createElement("li");
    li.textContent = "Connected";
    document.getElementById("messagesList").appendChild(li);
}

function OnClose() {
    const li = document.createElement("li");
    li.textContent = "Disconnected";
    document.getElementById("messagesList").appendChild(li);
}

gasBayConnection.on("RecieveAutoReading", function (data) {
    updateData(data);
    gasBayStore.push([{ type: "update", key: data.Identifier, data: boxReadingData }]);
});

function generateData(dto) {
    for (var x = 0; x < dto.columns.length; x++) {
        boxReadingData.map((d) => d[dto.columns[x].header] = dto.row[dto.columns[x].columnName]);
    }
}

function updateData(dto) {
    for (var x = 0; x < dto.columns.length; x++) {
        boxReadingData.map((d) => d[dto.columns[x].header] = dto.row[dto.columns[x].columnName]);
    }
}

gasBayConnection.on("SetMaintenanceCallBack",(result) => {
    const li = document.createElement("li");
    if(result===true) {
        li.textContent = "Switched to Maintenance";
    } else {
        li.textContent = "Error Switching to Maintenance";
    }
    document.getElementById("messagesList").appendChild(li);
});

gasBayConnection.onclose(async () => {
    await startGasBay();
    OnClose();
});

$(async function () { await startGasBay();});
