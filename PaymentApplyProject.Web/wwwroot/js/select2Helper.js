(function ($) {
    $.fn.serverSelect2 = function ({ url, tags = false, type = 'get', extraData = null, extraOptions = null, pagingLength = 50, width = '100%' }) {
        let options = {
            language: 'tr',
            tags: tags,
            ajax: {
                url: `/select/${url}`,
                type: type,
                dataType: 'json',
                delay: 500,
                data: function (params) {
                    let returnValue = {
                        search: params.term || '',
                        page: params.page || 0,
                        pageLength: pagingLength
                    };
                    if (Array.isArray(extraData)) {
                        extraData.forEach((item) => {
                            returnValue[item.name] = item.value;
                        });
                    }
                    return returnValue;
                },
                processResults: function (data, params) {
                    params.page = params.page || 1;

                    if (params.page === 1 && Array.isArray(extraOptions)) {
                        extraOptions.forEach((item) => {
                            data.items.unshift(item);
                        });
                    }
                    return {
                        results: data.items,
                        pagination: {
                            more: (params.page * pagingLength) < data.count
                        }
                    };
                },
                cache: true
            },
            placeholder: 'Arama yapın.',
            //minimumInputLength: 3,
            width: width
        };

        // Apply select2 to each element in the jQuery collection
        return this.each(function () {
            $(this).select2(options);
        });
    };
})(jQuery);