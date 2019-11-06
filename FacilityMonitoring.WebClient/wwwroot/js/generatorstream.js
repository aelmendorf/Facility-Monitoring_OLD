const generatorConnection = new signalR.HubConnectionBuilder()
    .withUrl("http://172.20.4.209:443/hubs/generators")
    //.withUrl("http://localhost:5000/hubs/generators")
    .configureLogging(signalR.LogLevel.Information)
    .build();

var generatorStore = new DevExpress.data.CustomStore({
    load: function () {
        return generatorConnection.invoke("GetAllGenerators");
    }, key: "identifier"
});

async function startGenerator() {
    try {
        await generatorConnection.start();
        console.log("Connected!");
        buildGeneratorTable();
    } catch (err) {
        console.log(err);
        setTimeout(() => startGenerator(), 5000);
    }
}

function buildGeneratorTable() {

    $("#genReadings").dxDataGrid({
        dataSource: generatorStore,
        visible: true
    });
}

generatorConnection.on("SendGeneratorReading", function (data) {
    generatorStore.push([{ type: "update", key: data.identifier, data: data }]);
});

generatorConnection.onclose(async () => {
    await startGenerator();
});

$(async function () { await startGenerator(); });