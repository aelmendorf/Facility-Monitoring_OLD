
const tankScaleConnection = new signalR.HubConnectionBuilder()
    .withUrl("http://172.20.4.209:443/hubs/tankscales")
    //.withUrl("http://localhost:5000/hubs/tankscales")
    .configureLogging(signalR.LogLevel.Information)
    .build();

var ammoniaStore = new DevExpress.data.CustomStore({
    load:async function () {
        return await tankScaleConnection.invoke("GetData");
    }, key: "identifier"
});

async function startTankScale() {
    try {
        await tankScaleConnection.start();
        console.log("Connected!");
        buildAmmoniaTable();
    } catch (err) {
        console.log(err);
        setTimeout(async () => await startTankScale(), 5000);
    }
}

function buildAmmoniaTable() {
    $("#tankReadings").dxDataGrid({
        dataSource: ammoniaStore,
        visible: true
    });
}

function updateData(dto) {
    for (var x = 0; dto.columns.length; x++) {

    }
}

tankScaleConnection.on("RecieveAutoReading", function (data) {
    ammoniaStore.push([{ type: "update", key: data.identifier, data: data }]);
});

tankScaleConnection.onclose(async () => {
    await startTankScale();
});

$(async function () { await startTankScale(); });
