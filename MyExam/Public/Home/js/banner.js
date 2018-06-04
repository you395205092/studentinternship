
		/*mainbanner*/
		var Banneri = -1;
		var BannerTime= 4000 ;
		var BannerTimer;
		var BannerTotal;
		var Bannering = false
		$(function(){
			$("#Banner").append("<ul class='btn'></ul>");

			BannerTotal = $("#Banner .pic li").length;

			for(i = 0; i<BannerTotal;i++){
			$("#Banner .btn").append("<li></li>");
			}	


		$("#Banner .btn li").hover(function(){
			BanliShow($(this).index());
			},function(){
			return ;
			})

		$("#Banner .text li").hover(function(){
			BanliShow($(this).index());
			},function(){
			return ;
			})
		$("#Banner").hover(function(){
			clearTimeout(BannerTimer);
			},function(){
				BannerTimer = setTimeout(BanliAuto,BannerTime);
				})
		BanliShow(0);


		BannerTimer = setTimeout(BanliAuto,BannerTime)		
		});
		function BanliAuto(){
			var TemID = Banneri+1;
			TemID = TemID>=BannerTotal?0:TemID;	
			BanliShow(TemID);
			clearTimeout(BannerTimer);
			BannerTimer = setTimeout(BanliAuto,BannerTime);
		}
		function BanliShow(TemID){
			if(Bannering || TemID == Banneri)
				return;
			Bannering = true;
			if(Banneri >=0)
				$("#Banner .pic li").eq(Banneri).css("z-index",2);	
				$("#Banner .pic li").eq(TemID).css("z-index",3);
				$("#Banner .btn li").removeClass("s");
				$("#Banner .btn li").eq(TemID).addClass("s");	
				Banneri = TemID;
				$("#Banner .pic li").stop();
				$("#Banner .pic li").eq(Banneri).fadeIn(800,function(){
					$("#Banner .pic li").each(function(i) {	
					  Bannering = false	
					  if( i != Banneri){
						  $("#Banner .pic li").eq(i).css("z-index",1);
						  $("#Banner .pic li").eq(i).fadeOut();	
					  }
				  });		
			});	
		}
