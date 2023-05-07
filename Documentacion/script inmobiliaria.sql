create database inmobiliria
go
use inmobiliria
go
Create table tbl_inmueb(	
	inm_id int not null identity (1,1),
	suc_id int not null, --fk
	tpi_id int not null, --fk
	per_id int not null, --fk
	est_id int not null, --fk

	imn_refere varchar(50) not null,
	imn_direcc varchar(250) not null,
	imn_superf int not null,
	imn_nhabit tinyint not null default 1,
	imn_nbanio tinyint not null default 0,
	imn_ncocin tinyint not null default 0,
	imn_tngas bit not null,
	imn_tnparq bit not null,
	constraint pk_tbl_inmueb Primary key(inm_id)
);

Create table tbl_sucurs(
	suc_id int not null,
	est_id int not null, --fk

	suc_nombre varchar(100) not null,
	suc_direcc varchar(250) not null,
	suc_telefo varchar(50) not null,
	constraint pk_tbl_sucurs Primary key(suc_id)
);

Create table tbl_imagen(
	img_id int not null identity (1,1),
	img_ruta varchar(250) not null,
	ofe_id int not null, --fk
	constraint pk_tbl_imagen Primary key(img_id)
);

Create table tbl_tpinmu(
	tpi_id int not null,
	tpi_nombre varchar(100) not null,
	constraint pk_tbl_tpinmu Primary key(tpi_id)
);

Create table tbl_estado(
	est_id int not null,
	est_nombre varchar(100) not null,
	constraint pk_tbl_estado Primary key(est_id)
);
Create table tbl_tptran(
	tpt_id int not null,
	tpt_nombre varchar(100) not null,
	constraint pk_tbl_tptran Primary key(tpt_id)
);
Create table tbl_oferta(
	ofe_id int not null identity (1,1),
	inm_id int not null, --fk
	est_id int not null, --fk
	
	ofe_fecini date not null,
	ofe_fecfin date null,
	ofe_movent money null,
	ofe_moalqu money null,
	ofe_esvent bit not null,
	ofe_esalqu bit not null,	
	constraint pk_tbl_oferta Primary key(ofe_id)
);
Create table tbl_transa(
	tra_id int not null identity (1,1),
	inm_id int not null, --fk
	tpt_id int not null, --fk
	per_id int not null, --fk
	est_id int not null, --fk

	tra_fecha date not null,
	tra_monto money not null,
	constraint pk_tbl_transa Primary key(tra_id)
);
Create table tbl_person(

	per_id int not null identity (1,1),
	
	per_identi varchar(20) not null,
	per_nombre varchar(100) not null,
	per_apelli varchar(100) not null,
	per_telefo varchar(50) not null,
	per_direcc varchar(250) not null,
	per_email varchar(250) not null,
	per_paswor varchar(50) not null,
	constraint pk_tbl_person Primary key(per_id)
);
Create table tbl_visita(
	vis_id int not null identity (1,1),
	ofe_id int not null, --fk
	per_id int not null, --fk
	
	vis_fecha datetime not null,
	vis_realiz bit not null,
	vis_coment varchar(500) null,
	constraint pk_tbl_visita Primary key(vis_id)
);
go
alter table tbl_inmueb add constraint fk_inm_suc foreign key (suc_id) references tbl_sucurs (suc_id);
alter table tbl_inmueb add constraint fk_inm_tpi foreign key (tpi_id) references tbl_tpinmu (tpi_id);
alter table tbl_inmueb add constraint fk_inm_per foreign key (per_id) references tbl_person (per_id);

alter table tbl_oferta add constraint fk_ofe_inm foreign key (inm_id) references tbl_inmueb (inm_id);

alter table tbl_transa add constraint fk_tra_inm foreign key (inm_id) references tbl_inmueb (inm_id);
alter table tbl_transa add constraint fk_tra_tpt foreign key (tpt_id) references tbl_tptran (tpt_id);
alter table tbl_transa add constraint fk_tra_per foreign key (per_id) references tbl_person (per_id);

alter table tbl_visita add constraint fk_vis_ofe foreign key (ofe_id) references tbl_oferta (ofe_id);
alter table tbl_visita add constraint fk_vis_per foreign key (per_id) references tbl_person (per_id);

alter table tbl_imagen add constraint fk_img_ofe foreign key (ofe_id) references tbl_oferta(ofe_id);

alter table tbl_inmueb add constraint fk_inm_est foreign key (est_id) references tbl_estado(est_id);
alter table tbl_sucurs add constraint fk_suc_est foreign key (est_id) references tbl_estado(est_id);
alter table tbl_oferta add constraint fk_ofe_est foreign key (est_id) references tbl_estado(est_id);
alter table tbl_transa add constraint fk_tra_est foreign key (est_id) references tbl_estado(est_id);