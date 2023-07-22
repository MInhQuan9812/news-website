// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//$(document).ready(function () {
//    addNewArticle();
//})


//function addNewArticle() {
//    const articleView = document.getElementById('content');
//    articleView.innerHTML = `
//        <h4>(VTC News) - Cửa sổ máy bay hay tàu hoả đều được thiết kế bo tròn, có hình bầu dục, điều này liên quan gì đến an toàn hàng không và đường sắt?</h4>
//	    <p>Mọi chi tiết trên máy bay hay tàu hoả đều được thiết kế và tính toán kỹ lưỡng. Có lẽ đó là lý do đây là các phương tiện an toàn nhất để di chuyển đến những nơi xa. </p>
//        <p>Nếu để ý bạn sẽ thấy cửa sổ được thiết kế trên các phương tiện này đều có hình bầu dục và bo tròn. Chi tiết tuy nhỏ này từng không được các nhà thiết kế quan tâm nhưng lại là nguyên nhân khiến máy bay gặp nạn.</p>
//    	<h4>Vì sao cửa kính máy bay và tàu hoả có hình bầu dục?</h4>
//	    <p>Vào đầu những năm 1950, máy bay phản lực thương mại đầu tiên trên thế giới đã được phát triển và sản xuất bởi hãng De Havilland. Đây được xem là phép màu của ngành hàng không. Tuy nhiên, sau một năm sử dụng, chiếc máy bay bị rơi ra từng mảnh ngay trên bầu trời và trong vài tháng sau đó có thêm 2 chiếc nữa chịu chung số phận.</p>
//        <img src="https://kenh14cdn.com/thumb_w/660/2020/5/28/0-1590653959375414280410.jpg" alt="Red dot" />
//    `
//}

function showPassword() {
    var x = document.getElementById("password");
    if (x.type === "password") {
        x.type = "text";
    } else {
        x.type = "password";
    }
}