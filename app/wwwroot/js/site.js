// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
  getDatatable("#table-users");
});

function getDatatable(id) {
  $(id).DataTable({
    ordering: true,
    paging: true,
    searching: true,
    oLanguage: {
      sEmptyTable: "Nenhum registro encontrado na tabela",
      sInfo: "Mostrar START até END de TOTAL registros",
      sInfoEmpty: "Mostrar 0 até 0 de 0 Registros",
      sInfoFiltered: "(Filtrar de MAX total registros)",
      sInfoPostFix: "",
      sInfoThousands: ".",
      sLengthMenu: "Mostrar MENU registros por pagina",
      sLoadingRecords: "Carregando...",
      sProcessing: "Processando...",
      sZeroRecords: "Nenhum registro encontrado",
      sSearch: "Pesquisar",
      oPaginate: {
        sNext: "Proximo",
        sPrevious: "Anterior",
        sFirst: "Primeiro",
        sLast: "Ultimo",
      },
      oAria: {
        sSortAscending: ": Ordenar colunas de forma ascendente",
        sSortDescending: ": Ordenar colunas de forma descendente",
      },
    },
  });
}

$(".close-alert").click(function () {
  $(".alert").hide("hide");
});
