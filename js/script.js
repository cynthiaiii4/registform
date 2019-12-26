grecaptcha.ready(function() {
	grecaptcha
		.execute('6Lf6pL0UAAAAAPo6jQwDUh0xSSafY0FPoTt86qRL', {
			action: 'social'
		})
		.then(function(token) {
			console.log(token);
			document.getElementById('hiddenToken').value = token;
			const tokenId = document.getElementById('hiddenToken').value;
			const xhr = new XMLHttpRequest();
			xhr.open('POST', 'verification.ashx');
			// xhr.setRequestHeader("Content-Type", "application/json");
			xhr.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded'); // 要用 x-www-form-urlencoded 方式傳送
			xhr.onload = function() {
				const res = this.responseText;
				console.log(this.responseText);
			};
			xhr.send(`hiddenToken=${tokenId}`);
		});
});

const btn = document.querySelector('.form__btn');
btn.addEventListener('click', sendForm, false);

function sendForm() {
	//表單資訊
	const name = document.querySelector('#name').value;
	const email = document.querySelector('#email').value;
	const tel = document.querySelector('#tel').value;
	const pdf = document.getElementById('pdf').value;
	const introduction = document.querySelector('#introduction').value;
	const why = document.querySelector('#why').value;
	const essay = document.querySelector('#essay').value;
	const tokenId = document.getElementById('hiddenToken').value;
	// 取得組別
	const classType = document.querySelectorAll('.classType');
	let myClass;
	classType.forEach((element) => {
		if (element.checked == true) {
			myClass = element.value;
		}
	});
	// 取得梯次
	const classDate = document.querySelectorAll('.classDate');
	let myDate = [];
	classDate.forEach((element) => {
		if (element.checked == true) {
			myDate.push(element.value);
		}
	});
	myDate = myDate.join();

	let formData = {
		name,
		email,
		tel,
		pdf,
		introduction,
		why,
		essay,
		joingroup: myClass,
		batch: myDate
	};
	console.log(formData);
	
	formData = JSON.stringify(formData);
	console.log(formData);
	const answer = `formData = ${formData}`;
	const xhr = new XMLHttpRequest();
	xhr.open('POST', 'registform.ashx');
	xhr.setRequestHeader('Content-Type', 'application/json');
	xhr.onload = function() {
		const res = this.responseText;
		console.log(this.responseText);
		const form = document.querySelector('.form');
		if (res == 'success') {
			// 表單成功內容
			form.innerHTML = `
                <div class="sendResult ">
                    <div class="firework animated tada">
                        <img class="firework01" src="img/icon_star02.png" alt="">
                        <img class="firework02" src="img/icon_star01.png" alt="">
                    </div>
                    <h2 class="animated bounceIn">YA!表單送出成功</h2>
                    <p>我們收到您的報名信件後，<br />會再進行審核，並挑選第一波面試名單，<br />經面試後審核沒問題，<br />符合資格便可加入火箭隊培訓營囉！</p>
                </div>`;
			document.querySelector('.rocket').style.animationName = 'rocket_fly';
		} else {
			// 表單失敗
			form.innerHTML = `
            <div class="sendResult ">
                <div class="animated tada icon_cry">
                    <img class="firework01" src="img/cry.png" alt="">
                </div>
                <h2 class="animated bounceIn">嗚嗚～表單送出失敗</h2>
                <p>${res}</p>
            </div>
            <a href="index.html" class="form__btn">重新填寫</a>
            `;
		}
	};
	//xhr.send(answer);
	xhr.send("formData ={aaa:123}");
}
