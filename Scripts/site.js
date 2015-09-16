
var queryString = [], hash;
var q = document.URL.split('?')[1];
if(q != undefined){
    q = q.split('&');
    for(var i = 0; i < q.length; i++){
        hash = q[i].split('=');
        queryString.push(hash[1]);
        queryString[hash[0]] = hash[1];
    }
}

function navigate(lodloi, value) {
    var notation = queryString['notation'];
    switch(lodloi)
    {
        case 'lod':
            var params = {'notation' : notation, 'lod' : value, 'loi' : loiLevel};
            var uri =  window.location.pathname + '?' + $.param(params);
            break;
        case 'loi':
            var params = {'notation' : notation, 'lod' : lodLevel, 'loi' : value};
            var uri = window.location.pathname + '?' + $.param(params);
            break;
    }
    location.href = uri;
}