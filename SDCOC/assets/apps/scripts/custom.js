$(window).load(function () {
    $('#frmLogin').addClass('login-form');
    $('#frmForgetPassword').addClass('forget-form');    
  //  $('#sample_editable_1').dataTable();

    if ($('#sample_editable_1_length .form-control').hasClass('input-sm')) {
        $('#sample_editable_1_length .form-control').removeClass('input-sm');
    }
});