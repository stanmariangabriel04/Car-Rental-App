window.addEventListener('scroll',()=>{
    const n=document.getElementById('mainNavbar');
    if(n) n.style.boxShadow=window.scrollY>20?'0 2px 20px rgba(0,0,0,.4)':'';
});
setTimeout(()=>{
    document.querySelectorAll('.alert.position-fixed').forEach(el=>{
        try{bootstrap.Alert.getOrCreateInstance(el)?.close();}catch(e){}
    });
},4000);
