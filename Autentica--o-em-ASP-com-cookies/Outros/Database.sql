/*DROP database dbAut;*/

create database dbAut;
use dbAut;

create table tbAut(
UsuarioID int primary key auto_increment,
UsuNome varchar(100) not null,
Login varchar(100) not null unique,
Senha varchar(100) not null
);

DELIMITER $$
create procedure InsertUsuario(
UsuNomep varchar(100),
Loginp varchar(100),
Senhap varchar(100))
begin
	insert into tbAut (UsuNome, Login, Senha)
    values (UsuNomep, Loginp, Senhap);
end; $$

DELIMITER $$
create procedure SelectLogin(
Loginp varchar(100))
begin
	select Login from tbAut where (Login = Loginp);
end
$$

DELIMITER $$
create procedure SelectUsuario(
UsuNomep varchar(100))
begin
	select * from tbAut where (UsuNome = UsuNomep);
end
$$

DELIMITER $$
create procedure UpdateSenha(
Loginp varchar(100),
NewSenhap varchar(100))
begin
	update tbAut set Senha = NewSenhap where Login = Loginp;
end
$$

call InsertUsuario ("Madu Gaspar","madus@gmail","1234madu");
call SelectLogin ("madus@gmail");
call SelectLogin ("mariana@gmail");
call SelectUsuario ("Mari Feike");
call UpdateSenha ("madu@gmail","novaMadu");
call SelectUsuario ("Madu Gaspar");
call UpdateSenha ("madus@gmail","OutraMadu");
call SelectUsuario ("Madu Gaspar");