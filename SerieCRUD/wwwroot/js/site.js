function deleteConfirmation(id) {
    swal({
        title: "Tem certeza?",
        type: "warning",
        showCancelButton: true,
        confirmButtonClass: "btn-danger",
        confirmButtonText: "Deletar",
        cancelButtonText: "Cancelar",        
    },
    function(willDelete){
        if (willDelete) {
            $.ajax({
                url: '/Home/Delete',
                data: { id: id },   
                success: function (msg) {
                    $("#mydiv").load(location.href + " #mydiv>*", "");
                    toastr.success("Operação realizada com sucesso.");
                }
            });
        }
    });
}

$(document).ready(function () {
    $('body').on('click', '.btnModal',function () {
        var id = $(this).data("value");
        if (id == null) {
            $('#myModal .modal-title').html("Adicionar");
        }
        else {
            $('#myModal .modal-title').html("Editar");
        }    
        $("#teste").load("/Home/Upsert/" + id, function () {
            $('#myModal').modal("show");
            $.validator.unobtrusive.parse($("form"));
        });
    });
});

jQueryAjaxPost = form => {
    try {
        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
                $('#myModal .modal-body').html('');
                $('#myModal .modal-title').html('');
                $('#myModal').modal('hide');
                $("#mydiv").load(location.href + " #mydiv>*", "");
                $("#myModal").load(location.href + " #myModal>*", "");
                toastr.success("Operação realizada com sucesso.");
            }
        })
        //to prevent default form submit event
        return false;
    } catch (ex) {
        console.log(ex)
    }
}
