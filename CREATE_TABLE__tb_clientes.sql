CREATE TABLE IF NOT EXISTS `mydb`.`tb_clientes` (
  `nm_email` VARCHAR(50) NOT NULL,
  `pwd_senha` VARCHAR(25) NOT NULL,
  `nm_cliente` VARCHAR(50) NOT NULL,
  `dt_nascimento` VARCHAR(45) NULL,
  `flg_contribuidor` TINYINT NOT NULL,
  `id_cliente` INT NOT NULL AUTO_INCREMENT,
  UNIQUE INDEX `email_UNIQUE` (`nm_email` ASC) VISIBLE,
  PRIMARY KEY (`id_cliente`),
  UNIQUE INDEX `id_cliente_UNIQUE` (`id_cliente` ASC) VISIBLE)
ENGINE = InnoDB