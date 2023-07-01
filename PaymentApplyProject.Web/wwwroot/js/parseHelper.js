const parser = {
    moneyToFloat: (money) => parseFloat(money.replaceAll('.', '').replaceAll(',', '.'))
}