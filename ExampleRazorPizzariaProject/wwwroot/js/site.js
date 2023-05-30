		$('.chkclass').click(function() {
			var sum = baseValueToSumPizzaTotalPrice;
			$('.chkclass:checked').each(function() {
				sum += parseFloat($(this).closest('div').find('.valueToSum').text());
			});
			$('#sum').text(sum);
		});