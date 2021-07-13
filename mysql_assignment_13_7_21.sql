/*
SQLyog Community v13.1.6 (64 bit)
MySQL - 5.7.21 : Database - pactice_db
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;
CREATE DATABASE /*!32312 IF NOT EXISTS*/`pactice_db` /*!40100 DEFAULT CHARACTER SET utf8 */;

USE `pactice_db`;

/*Table structure for table `categories` */

DROP TABLE IF EXISTS `categories`;

CREATE TABLE `categories` (
  `cat_id` int(11) NOT NULL AUTO_INCREMENT,
  `cat_name` varchar(45) NOT NULL,
  `parent_category` varchar(45) NOT NULL DEFAULT '0',
  PRIMARY KEY (`cat_id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=latin1;

/*Data for the table `categories` */

insert  into `categories`(`cat_id`,`cat_name`,`parent_category`) values 
(1,'engineering','0'),
(2,'Computer','1'),
(3,'IT','1'),
(4,'MCA','0'),
(5,'networking','3');

/*Table structure for table `course` */

DROP TABLE IF EXISTS `course`;

CREATE TABLE `course` (
  `c_id` int(11) NOT NULL AUTO_INCREMENT,
  `c_name` varchar(45) NOT NULL,
  `c_cradit` int(11) NOT NULL,
  PRIMARY KEY (`c_id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=latin1;

/*Data for the table `course` */

insert  into `course`(`c_id`,`c_name`,`c_cradit`) values 
(1,'DBMS',8),
(2,'OS',10),
(3,'DS',10),
(4,'ALGO',12),
(5,'WEB',8),
(6,'CN',10);

/*Table structure for table `course_category_mapping` */

DROP TABLE IF EXISTS `course_category_mapping`;

CREATE TABLE `course_category_mapping` (
  `cc_id` int(11) NOT NULL AUTO_INCREMENT,
  `course_id` int(11) NOT NULL,
  `category_id` int(11) NOT NULL,
  PRIMARY KEY (`cc_id`),
  KEY `course_id_idx` (`course_id`),
  KEY `category_id_idx` (`category_id`),
  CONSTRAINT `category_id` FOREIGN KEY (`category_id`) REFERENCES `categories` (`cat_id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `course_id` FOREIGN KEY (`course_id`) REFERENCES `course` (`c_id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=17 DEFAULT CHARSET=latin1;

/*Data for the table `course_category_mapping` */

insert  into `course_category_mapping`(`cc_id`,`course_id`,`category_id`) values 
(9,1,2),
(10,2,2),
(11,2,3),
(12,5,3),
(13,1,4),
(14,5,4),
(16,6,5);

/*Table structure for table `student_course_mapping` */

DROP TABLE IF EXISTS `student_course_mapping`;

CREATE TABLE `student_course_mapping` (
  `sc_id` int(11) NOT NULL AUTO_INCREMENT,
  `student_id` int(11) NOT NULL,
  `course_id` int(11) NOT NULL,
  PRIMARY KEY (`sc_id`),
  KEY `student_id_idx` (`student_id`),
  KEY `course_id_idx` (`course_id`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=latin1;

/*Data for the table `student_course_mapping` */

insert  into `student_course_mapping`(`sc_id`,`student_id`,`course_id`) values 
(1,1,1),
(2,1,2),
(3,1,3),
(4,2,2),
(5,2,3),
(6,2,5),
(7,1,6);

/*Table structure for table `students` */

DROP TABLE IF EXISTS `students`;

CREATE TABLE `students` (
  `sid` int(5) NOT NULL AUTO_INCREMENT,
  `s_name` varchar(10) DEFAULT NULL,
  `s_number` varchar(15) DEFAULT NULL,
  `s_address` varchar(40) DEFAULT NULL,
  `s_dob` date DEFAULT NULL,
  PRIMARY KEY (`sid`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1;

/*Data for the table `students` */

insert  into `students`(`sid`,`s_name`,`s_number`,`s_address`,`s_dob`) values 
(1,'darsh','9054229929','mehsana','2000-08-19'),
(2,'nishit','1234567890','surat','2000-08-19');

/* Procedure structure for procedure `get_details_by_category` */

/*!50003 DROP PROCEDURE IF EXISTS  `get_details_by_category` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `get_details_by_category`(
    IN  categoryName  VARCHAR(45)
)
BEGIN
	DECLARE exit handler for SQLEXCEPTION
	 BEGIN
	  GET DIAGNOSTICS CONDITION 1 @sqlstate = RETURNED_SQLSTATE, 
	   @errno = MYSQL_ERRNO, @text = MESSAGE_TEXT;
	  SET @error_stmt = CONCAT("ERROR ", @errno, " (", @sqlstate, "): ", @text);
	  SELECT @error_stmt;
	 END;
	IF categoryName= '' then
		SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Category can\'t be empty,Please enter category.';
	end if;
    if  cast(categoryName AS nchar) then
     SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Please enter valid category name';
    End if;
     IF NOT EXISTS (select s_name as student_name,s_address as address,s_number as mobile_number,s_dob as date_of_birth,c_name as course_name,c_cradit as course_cradit from course,students inner join
    (select student_id ,course_id from student_course_mapping scm
		where scm.course_id IN (select course_id from course_category_mapping ccm 
		where ccm.category_id = (select cat_id from categories where cat_name = categoryName))) as sc
        where 
	sid = sc.student_id and c_id = sc.course_id) THEN
	    SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'No Record Found on that Category';
	 ELSE
		select s_name as student_name,s_address as address,s_number as mobile_number,s_dob as date_of_birth,c_name as course_name,c_cradit as course_cradit from course,students inner join
    (select student_id ,course_id from student_course_mapping scm
		where scm.course_id IN (select course_id from course_category_mapping ccm 
		where ccm.category_id = (select cat_id from categories where cat_name = categoryName))) as sc
        where 
	sid = sc.student_id and c_id = sc.course_id;
	 END IF;
	
	
END */$$
DELIMITER ;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
