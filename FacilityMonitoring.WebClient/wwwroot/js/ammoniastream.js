
const connection = new signalR.HubConnectionBuilder()
    .withUrl("http://172.20.4.209:443/hubs/ammonia")
    //.withUrl("http://localhost:5000/hubs/ammonia")
    .configureLogging(signalR.LogLevel.Information)
    .build();

var ammoniaStore = new DevExpress.data.CustomStore({
    load:async function () {
        return await connection.invoke("GetData");
    }, key: "identifier"
});

async function startAmmonia() {
    try {
        await connection.start();
        console.log("Connected!");
        buildAmmoniaTable();
    } catch (err) {
        console.log(err);
        setTimeout(() => startAmmonia(), 5000);
    }
}

function buildAmmoniaTable() {
    $("#tankReadings").dxDataGrid({
        dataSource: ammoniaStore,
        visible: true
    });
}

connection.on("RecieveAutoReading", function (data) {
    ammoniaStore.push([{ type: "update", key: data.identifier, data: data }]);
});

connection.onclose(async () => {
    await startAmmonia();
});

$(async function () { await startAmmonia(); });
