function CreateOrAddNewDocument(e) {
    e.preventDefault();

    var id = document.getElementById('ID').value;

    var code = document.getElementById('Code').value;
    var title = document.getElementById('Title').value;
    var review = document.getElementById('Review').value;
    var plannedDate = document.getElementById('PlannedDate').value;
    var value = document.getElementById('Value').value.replace('.', ',');

    $.post("/Document/SaveNewOrUpdateDocument", { ID: id, Code: code, Title: title, Review: review, PlannedDate: plannedDate, Value: value },
        function (res) {
            $("#DocumentListArea").empty();
            $("#DocumentListArea").html(res);
        }).fail(function (res) {
            alert(res.msg);
        });
}

function ShowCreateEditModel(id, code, title, review, plannedDate, value) {
    if (id != 0) {
        document.getElementById('CreateAndEditModelLabel').innerText = "Editar Documento";
        document.getElementById('ID').value = id;

        document.getElementById('Code').value = code;
        document.getElementById('Title').value = title;
        document.getElementById('Review').value = review;
        document.getElementById('PlannedDate').valueAsDate = new Date(plannedDate);
        document.getElementById('Value').value = value.replace(',', '.');

    } else {
        document.getElementById('CreateAndEditModelLabel').innerText = "Adicionar Novo Documento";
        document.getElementById('ID').value = 0;

        document.getElementById('Code').value = '';
        document.getElementById('Title').value = '';
        document.getElementById('Review').value = 1;
        document.getElementById('PlannedDate').valueAsDate = new Date();
        document.getElementById('Value').value = 0;
    }
}

function ShowDocumentInformations(id) {
    if (id > 0) {
        $.get("/Document/GetDocumentById/" + id,
            function (res) {
                if (res.innerText != "") {
                    var result = JSON.stringify(res);

                    var json = result.replace("'", "\"");
                    var json = json.replace("'", "\"");
                    var obj = JSON.parse(json);

                    document.getElementById("detailsTitle").innerText = obj.Title;
                    document.getElementById("detailsCode").innerText = obj.Code;
                    document.getElementById("detailsReview").innerText = obj.ReviewLabel;
                    document.getElementById("detailsPlannedDate").innerText = obj.PlannedDateLabel;
                    document.getElementById("detailsValue").innerText = obj.Value.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' });

                }
            }).fail(function (res) {
                alert(res.msg);
            });
    }
}

function ArchiveDocumentOrDelete(id) {
    if (id > 0) {
        $.get("/Document/ArchiveDocument/" + id,
            function (res) {
                $("#DocumentListArea").empty();
                $("#DocumentListArea").html(res);
            }).fail(function (res) {
                alert(res.msg);
            });

    }
}

function ShowArchivedDocument(isArchived) {
    $.get("/Document/DocumentParcialList", { isArchived: isArchived },
        function (res) {
            $("#DocumentListArea").empty();
            $("#DocumentListArea").html(res);
            if (isArchived) {
                document.getElementById('btnShowArchivedDocument').innerText = "Ver Documentos Normais";
                document.getElementById('btnShowArchivedDocument').setAttribute("class", "btn btn-info mt-1");
                document.getElementById('btnShowArchivedDocument').setAttribute("onclick", "ShowArchivedDocument(false)");

            } else {
                document.getElementById('btnShowArchivedDocument').innerText = "Ver Documentos Arquivados";
                document.getElementById('btnShowArchivedDocument').setAttribute("class", "btn btn-danger mt-1");
                document.getElementById('btnShowArchivedDocument').setAttribute("onclick", "ShowArchivedDocument(true)");
            }

        }).fail(function (res) {
            alert(res.msg);
        });
}

function UnarchiveDocument(id) {
    if (id > 0) {
        $.get("/Document/UnarchiveDocument/" + id,
            function (res) {
                $("#DocumentListArea").empty();
                $("#DocumentListArea").html(res);
            }).fail(function (res) {
                alert(res.msg);
        });
    }
}

function DeleteDocument(id) {
    if (id > 0) {
        $.post("/Document/RemovePermanentlyDocument/" + id,
            function (res) {
                $("#DocumentListArea").empty();
                $("#DocumentListArea").html(res);
            }).fail(function (res) {
                alert(res.msg);
        });
    }
}