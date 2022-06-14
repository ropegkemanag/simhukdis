-- phpMyAdmin SQL Dump
-- version 5.1.3
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: May 13, 2022 at 10:48 AM
-- Server version: 10.4.22-MariaDB
-- PHP Version: 7.4.28

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `app_hukdis`
--

DELIMITER $$
--
-- Procedures
--
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_Disposisi1_Sel` ()   SELECT 
	a.id
	,a.nomor_agenda
	,a.nomor_surat
	,a.asal_surat
	,DATE_FORMAT(a.tanggal_surat, '%d-%m-%Y') AS tanggal_surat
	,a.perihal
	,a.LampiranSurat1
	,a.LampiranSurat2
	,a.LampiranSurat3
	,a.LampiranSurat4
	,a.LampiranSurat5
	,a.LampiranSurat6
	,CASE a.Dokumen_Yang_Akan_Dibuat when '01' then 'Telaah' else 'SK' end as Dokumen_Yang_Akan_Dibuat
	,c.Konseptor UserID
	,d.FullName konseptor
	,a.SatuanKerja AS KODE_SATUAN_KERJA
	,b.SATUAN_KERJA AS SatuanKerja
	,DATE_FORMAT(a.created_date, '%d-%m-%Y') AS created_date
	,a.create_user
	,DATE_FORMAT(a.update_date, '%d-%m-%Y') AS update_date
	,a.update_user
	,a.STATUS
	,DATE_FORMAT(a.Disposisi1_Date, '%d-%m-%Y') AS Disposisi1_Date
	,a.Disposisi1_By
	,CASE a.Disposisi1_Status WHEN '01' THEN 'TL' ELSE 'Lainnya' END AS Disposisi1_Status
	,a.Disposisi1_Notes
	,DATE_FORMAT(a.Disposisi2_Date, '%d-%m-%Y') AS Disposisi2_Date
	,Disposisi2_By	
	,CASE a.Disposisi2_Status WHEN '01' THEN 'TL' ELSE 'Lainnya' END AS Disposisi2_Status
	,a.Disposisi2_Notes
	,DATE_FORMAT(a.Disposisi3_Date, '%d-%m-%Y') AS Disposisi3_Date
	,Disposisi3_By	
	,CASE a.Disposisi3_Status WHEN '01' THEN 'TL' ELSE 'Lainnya' END AS Disposisi3_Status
	,a.Disposisi3_Notes
FROM kasus a
LEFT JOIN tbl_satker b ON a.SatuanKerja = b.KODE_SATUAN_KERJA
LEFT JOIN tbl_konseptor_satker c ON a.SatuanKerja = c.KODE_SATUAN_KERJA
LEFT JOIN tbl_user d ON d.UserID = c.Konseptor
WHERE STATUS = 1
	AND Disposisi1_Date IS NULL$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_Disposisi1_Upd` (IN `i_id` INT, IN `iStatus` INT(1), IN `iDisposisi_By` VARCHAR(25), IN `iDisposisi_Note` TEXT, IN `iDisposisi_Status` VARCHAR(25))   UPDATE kasus
SET STATUS = iStatus,
Disposisi1_By = iDisposisi_By,
Disposisi1_Notes = iDisposisi_Note,
Disposisi1_Status = iDisposisi_Status,
Disposisi1_Date = CURRENT_TIMESTAMP,
update_user = iDisposisi_By,
update_date = CURRENT_TIMESTAMP
WHERE ID = i_id$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_Disposisi2_Sel` ()   SELECT a.id
	,a.nomor_agenda
	,a.nomor_surat
	,a.asal_surat
	,DATE_FORMAT(a.tanggal_surat, '%d-%m-%Y') AS tanggal_surat
	,a.perihal
	,a.LampiranSurat1
	,a.LampiranSurat2
	,a.LampiranSurat3
	,a.LampiranSurat4
	,a.LampiranSurat5
	,a.LampiranSurat6
    ,CASE a.Dokumen_Yang_Akan_Dibuat when '01' then 'Telaah' else 'SK' end as Dokumen_Yang_Akan_Dibuat
	,c.Konseptor UserID
	,d.FullName konseptor
	,a.SatuanKerja AS KODE_SATUAN_KERJA
	,b.SATUAN_KERJA AS SatuanKerja
	,DATE_FORMAT(a.created_date, '%d-%m-%Y') AS created_date
	,a.create_user
	,DATE_FORMAT(a.update_date, '%d-%m-%Y') AS update_date
	,a.update_user
	,a.STATUS
	,DATE_FORMAT(a.Disposisi1_Date, '%d-%m-%Y') AS Disposisi1_Date
	,a.Disposisi1_By
	,CASE a.Disposisi1_Status WHEN '01' THEN 'TL' ELSE 'Lainnya' END AS Disposisi1_Status
	,a.Disposisi1_Notes
	,DATE_FORMAT(a.Disposisi2_Date, '%d-%m-%Y') AS Disposisi2_Date
	,Disposisi2_By	
	,CASE a.Disposisi2_Status WHEN '01' THEN 'TL' ELSE 'Lainnya' END AS Disposisi2_Status
	,a.Disposisi2_Notes
	,DATE_FORMAT(a.Disposisi3_Date, '%d-%m-%Y') AS Disposisi3_Date
	,Disposisi3_By	
	,CASE a.Disposisi3_Status WHEN '01' THEN 'TL' ELSE 'Lainnya' END AS Disposisi3_Status
	,a.Disposisi3_Notes
FROM kasus a
LEFT JOIN tbl_satker b ON a.SatuanKerja = b.KODE_SATUAN_KERJA
LEFT JOIN tbl_konseptor_satker c ON a.SatuanKerja = c.KODE_SATUAN_KERJA
LEFT JOIN tbl_user d ON d.UserID = c.Konseptor
WHERE STATUS = 2
	AND Disposisi2_Date IS NULL$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_Disposisi2_Upd` (IN `i_id` INT, IN `iStatus` INT(1), IN `iDisposisi_By` VARCHAR(25), IN `iDisposisi_Note` TEXT, IN `iDisposisi_Status` VARCHAR(25))   UPDATE kasus 
SET STATUS = iStatus, 
Disposisi2_By = iDisposisi_By, 
Disposisi2_Notes = iDisposisi_Note, 
Disposisi2_Date = CURRENT_TIMESTAMP,
Disposisi2_Status = iDisposisi_Status,
update_user = iDisposisi_By, 
update_date = CURRENT_TIMESTAMP WHERE ID = i_id$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_Disposisi3_Sel` ()   SELECT a.id
	,a.nomor_agenda
	,a.nomor_surat
	,a.asal_surat
	,DATE_FORMAT(a.tanggal_surat, '%d-%m-%Y') AS tanggal_surat
	,a.perihal
	,a.LampiranSurat1
	,a.LampiranSurat2
	,a.LampiranSurat3
	,a.LampiranSurat4
	,a.LampiranSurat5
	,a.LampiranSurat6
    ,CASE a.Dokumen_Yang_Akan_Dibuat when '01' then 'Telaah' else 'SK' end as Dokumen_Yang_Akan_Dibuat
	,c.Konseptor UserID
	,d.FullName konseptor
	,a.SatuanKerja AS KODE_SATUAN_KERJA
	,b.SATUAN_KERJA AS SatuanKerja
	,DATE_FORMAT(a.created_date, '%d-%m-%Y') AS created_date
	,a.create_user
	,DATE_FORMAT(a.update_date, '%d-%m-%Y') AS update_date
	,a.update_user
	,a.STATUS
	,DATE_FORMAT(a.Disposisi1_Date, '%d-%m-%Y') AS Disposisi1_Date
	,a.Disposisi1_By
	,CASE a.Disposisi1_Status WHEN '01' THEN 'TL' ELSE 'Lainnya' END AS Disposisi1_Status
	,a.Disposisi1_Notes
	,DATE_FORMAT(a.Disposisi2_Date, '%d-%m-%Y') AS Disposisi2_Date
	,Disposisi2_By	
	,CASE a.Disposisi2_Status WHEN '01' THEN 'TL' ELSE 'Lainnya' END AS Disposisi2_Status
	,a.Disposisi2_Notes
	,DATE_FORMAT(a.Disposisi3_Date, '%d-%m-%Y') AS Disposisi3_Date
	,Disposisi3_By	
	,CASE a.Disposisi3_Status WHEN '01' THEN 'TL' ELSE 'Lainnya' END AS Disposisi3_Status
	,a.Disposisi3_Notes
FROM kasus a
LEFT JOIN tbl_satker b ON a.SatuanKerja = b.KODE_SATUAN_KERJA
LEFT JOIN tbl_konseptor_satker c ON a.SatuanKerja = c.KODE_SATUAN_KERJA
LEFT JOIN tbl_user d ON d.UserID = c.Konseptor
WHERE STATUS = 3
	AND Disposisi3_Date IS NULL$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_Disposisi3_Upd` (IN `i_id` INT, IN `iStatus` INT(1), IN `iDisposisi_By` VARCHAR(25), IN `iDisposisi_Note` TEXT, IN `iDisposisi_Status` VARCHAR(25), IN `iKonseptor` VARCHAR(25), IN `iDokumen_Yang_Akan_Dibuat` VARCHAR(2))   UPDATE kasus 
SET STATUS = iStatus, 
Disposisi3_By = iDisposisi_By, 
Disposisi3_Notes = iDisposisi_Note, 
Disposisi3_Date = CURRENT_TIMESTAMP, 
Disposisi3_Status = iDisposisi_Status,
update_user = iDisposisi_By, 
update_date = CURRENT_TIMESTAMP ,
konseptor = iKonseptor,
Dokumen_Yang_Akan_Dibuat = iDokumen_Yang_Akan_Dibuat
WHERE ID = i_id$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_Disposisi_Sel` ()   SELECT * FROM kasus
WHERE Status = 1
and Disposisi_Date IS NULL$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_Disposisi_Upd` (IN `i_id` INT, IN `iStatus` INT(1), IN `iDisposisi_By` VARCHAR(25), IN `iDisposisi_Note` TEXT, IN `iKonseptor` VARCHAR(25))   UPDATE kasus
SET STATUS = iStatus,
Disposisi_By = iDisposisi_By,
Disposisi_Note = iDisposisi_Note,
Disposisi_Date = CURRENT_TIMESTAMP,
update_user = iDisposisi_By,
update_date = CURRENT_TIMESTAMP,
konseptor = iKonseptor
WHERE ID = i_id$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_Hukdis_AlreadyExist` (IN `iHukdisID` VARCHAR(25))   SELECT * FROM hukdis
WHERE HukdisID = iHukdisID$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_Hukdis_Del` (IN `iid` INT)   DELETE FROM HUKDIS 
WHERE id = iid$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_Hukdis_Ins` (IN `iHukdisID` VARCHAR(25), IN `iHukuman` TEXT, IN `iTingkat` VARCHAR(25), IN `iLastUser` VARCHAR(25))   INSERT INTO hukdis (HukdisID, hukuman,tingkat, LastUser, LastUpdate)
VALUES 
(iHukdisID, iHukuman, iTingkat, iLastUser, CURRENT_TIMESTAMP)$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_Hukdis_Sel` ()   SELECT 
id,
hukuman,
tingkat,
HukdisID,
LastUser,
DATE_FORMAT(LastUpdate,'%d-%m-%Y') as LastUpdate
FROM hukdis$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_Hukdis_Upd` (IN `iid` INT, IN `iHukdisID` VARCHAR(25), IN `iHukuman` TEXT, IN `iTingkat` VARCHAR(25), IN `iLastUser` VARCHAR(25))   UPDATE hukdis 
SET 
HukdisID = iHukdisID,
hukuman = iHukuman,
tingkat = iTingkat,
LastUser = iLastUser,
LastUpdate = CURRENT_TIMESTAMP
WHERE id = iid$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_Kasus_Monitoring_Sel` ()   SELECT  
id,
nomor_agenda,
nomor_surat,
asal_surat,
tanggal_surat,
perihal,
LampiranSurat1,
LampiranSurat2,
LampiranSurat3,
LampiranSurat4,
LampiranSurat5,
konseptor,
SatuanKerja,
Status,
Disposisi1_Date,
Disposisi1_By,
Disposisi1_Status,
Disposisi1_Notes,
Disposisi2_By,
Disposisi2_Date,
Disposisi2_Status,
Disposisi2_Notes,
Disposisi3_By,
Disposisi3_Date,
Disposisi3_Status,
Disposisi3_Notes
FROM kasus
order by tanggal_surat desc$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_Konseptor_Sel` ()   SELECT Fullname, UserID FROM tbl_user
WHERE GroupID = '02'
ORDER by fullname$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_PraDPK_Sel` ()   SELECT 
A.id,
B.NIP,
B.Fullname,
B.Dasar_dan_Bukti_Penunjang,
B.Pelanggaran,
B.Pasal_Pelanggaran,
B.Rekomendasi_Hukuman,
A.nomor_surat,
A.asal_surat,
a.SatuanKerja,
DATE_FORMAT(a.tanggal_surat, '%d-%m-%Y') AS tanggal_surat,
A.perihal
FROM kasus a 
LEFT JOIN tbl_telaah b	on a.id = b.ID
WHERE IFNULL(A.Status,0) = 5$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_Satker_AlreadyExists` (IN `iSATUAN_KERJA` VARCHAR(255))   SELECT * FROM tbl_satker
WHERE SATUAN_KERJA = iSATUAN_KERJA$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_Satker_Del` (IN `iKODE_SATUAN_KERJA` INT)   DELETE FROM tbl_satker WHERE KODE_SATUAN_KERJA = iKODE_SATUAN_KERJA$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_Satker_Ins` (IN `iSATUAN_KERJA` VARCHAR(255), IN `iUserID` VARCHAR(25))   insert into tbl_satker(SATUAN_KERJA, LastUser, LastUpdate)
VALUES
(iSATUAN_KERJA,iUserID,CURRENT_TIME)$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_Satker_InsUpd` (IN `iKODE_SATUAN_KERJA` INT, IN `iSATUAN_KERJA` TEXT)   INSERT INTO tbl_satker
  (KODE_SATUAN_KERJA, SATUAN_KERJA) 
VALUES 
  (iKODE_SATUAN_KERJA, iSATUAN_KERJA)
ON DUPLICATE KEY UPDATE
  SATUAN_KERJA = iSATUAN_KERJA$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_Satker_Konseptor_AlreadyExist` (IN `iKode_Satuan_Kerja` INT)   SELECT * FROM tbl_konseptor_satker
WHERE Kode_Satuan_Kerja = iKode_Satuan_Kerja$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_Satker_Konseptor_Delete` (IN `iSATUAN_KERJA` INT)   DELETE FROM tbl_konseptor_satker
WHERE KODE_SATUAN_KERJA = iSATUAN_KERJA$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_Satker_Konseptor_Ins` (IN `iUserID` INT, IN `iKODE_SATUAN_KERJA` INT, IN `iLastUser` VARCHAR(25))   INSERT INTO tbl_konseptor_satker (Konseptor, KODE_SATUAN_KERJA, LastUser, LastUpdate)
VALUES
(iUserID,iKODE_SATUAN_KERJA,iLastUser,CURRENT_TIMESTAMP())$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_Satker_Konseptor_Sel` ()   SELECT a.Konseptor, b.FullName, a.KODE_SATUAN_KERJA, c.SATUAN_KERJA FROM tbl_konseptor_satker a
LEFT join tbl_user b on a.Konseptor= b.UserID
left join tbl_satker c on a.KODE_SATUAN_KERJA = c.KODE_SATUAN_KERJA
ORDER BY A.Konseptor, A.LastUpdate$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_Satker_Konseptor_Upd` (IN `iKonseptor` INT, IN `iSATUAN_KERJA` INT)   UPDATE tbl_konseptor_satker
SET Konseptor = iKonseptor
WHERE KODE_SATUAN_KERJA = iSATUAN_KERJA$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_Satker_Sel` ()   SELECT * FROM tbl_satker
ORDER by KODE_SATUAN_KERJA$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_Satker_Upd` (IN `iKODE_SATUAN_KERJA` INT, IN `iSATUAN_KERJA` VARCHAR(255), IN `iUserID` VARCHAR(25))   UPDATE tbl_satker SET SATUAN_KERJA = iSATUAN_KERJA,
LastUser = iUserID, LastUpdate = CURRENT_TIME
WHERE KODE_SATUAN_KERJA = iKODE_SATUAN_KERJA$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_SuratMasuk_ByID_Sel` (IN `iid` INT)   SELECT a.id
	,a.nomor_agenda
	,a.nomor_surat
	,a.asal_surat
	,DATE_FORMAT(a.tanggal_surat, '%d-%m-%Y') AS tanggal_surat
	,a.perihal
	,a.LampiranSurat1
	,a.LampiranSurat2
	,a.LampiranSurat3
	,a.LampiranSurat4
	,a.LampiranSurat5
	,a.LampiranSurat6
	,a.Lampiran_LHA
    ,CASE a.Dokumen_Yang_Akan_Dibuat when '01' then 'Telaah' else 'SK' end as Dokumen_Yang_Akan_Dibuat
	,a.konseptor UserID
	,c.FullName konseptor
	,a.SatuanKerja AS KODE_SATUAN_KERJA
	,b.SATUAN_KERJA AS SatuanKerja
	,DATE_FORMAT(a.created_date, '%d-%m-%Y') AS created_date
	,a.create_user
	,DATE_FORMAT(a.update_date, '%d-%m-%Y') AS update_date
	,a.update_user
	,a.STATUS
	,DATE_FORMAT(a.Disposisi1_Date, '%d-%m-%Y') AS Disposisi1_Date
	,a.Disposisi1_By
	,CASE a.Disposisi1_Status when '01' then 'TL' else 'Lainnya' end Disposisi1_Status
	,a.Disposisi1_Notes
	,Disposisi2_By
	,DATE_FORMAT(a.Disposisi2_Date, '%d-%m-%Y') AS Disposisi2_Date
	,CASE a.Disposisi2_Status when '01' then 'TL' else 'Lainnya' end AS Disposisi2_Status
	,a.Disposisi2_Notes
	,Disposisi3_By
	,DATE_FORMAT(a.Disposisi3_Date, '%d-%m-%Y') AS Disposisi3_Date
	,CASE a.Disposisi3_Status when '01' then 'TL' else 'Lainnya' end AS Disposisi3_Status
	,a.Disposisi3_Notes
FROM kasus a
LEFT JOIN tbl_satker b ON a.SatuanKerja = b.KODE_SATUAN_KERJA
LEFT JOIN tbl_user c ON c.UserID = a.konseptor 
WHERE a.id = iid$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_SuratMasuk_Del` (IN `iid` INT)   DELETE FROM kasus WHERE id = iid$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_SuratMasuk_Display` ()   SELECT a.id
	,IFNULL(a.nomor_agenda,'') AS nomor_agenda
	,IFNULL(a.nomor_surat,'') AS nomor_surat
	,IFNULL(a.asal_surat,'') AS asal_surat
	,DATE_FORMAT(a.tanggal_surat, '%d-%m-%Y') AS tanggal_surat
	,IFNULL(a.perihal,'') AS perihal
	,IFNULL(a.LampiranSurat1,'') AS LampiranSurat1
	,IFNULL(a.LampiranSurat2,'') AS LampiranSurat2
	,IFNULL(a.LampiranSurat3,'') AS LampiranSurat3
	,IFNULL(a.LampiranSurat4,'') AS LampiranSurat4
	,IFNULL(a.LampiranSurat5,'') AS LampiranSurat5
	,IFNULL(a.LampiranSurat6,'') AS LampiranSurat6
	,IFNULL(a.Lampiran_LHA,'') AS Lampiran_LHA
	,CASE a.Dokumen_Yang_Akan_Dibuat
		WHEN '01'
			THEN 'Telaah'
		ELSE 'SK'
		END AS Dokumen_Yang_Akan_Dibuat
	,IFNULL(a.konseptor,'') AS UserID
	,IFNULL(c.FullName,'') AS konseptor
	,IFNULL(a.SatuanKerja,'') AS KODE_SATUAN_KERJA
	,IFNULL(b.SATUAN_KERJA,'') AS SatuanKerja
	,DATE_FORMAT(a.created_date, '%d-%m-%Y') AS created_date
	,IFNULL(a.create_user,'') AS create_user
	,DATE_FORMAT(a.update_date, '%d-%m-%Y') AS update_date
	,a.update_user
	,IFNULL(a.STATUS,0) STATUS
	,DATE_FORMAT(a.Disposisi1_Date, '%d-%m-%Y') AS Disposisi1_Date
	,a.Disposisi1_By
	,CASE a.Disposisi1_Status
		WHEN '01'
			THEN 'TL'
		ELSE 'Lainnya'
		END Disposisi1_Status
	,a.Disposisi1_Notes
	,Disposisi2_By
	,DATE_FORMAT(a.Disposisi2_Date, '%d-%m-%Y') AS Disposisi2_Date
	,CASE a.Disposisi2_Status
		WHEN '01'
			THEN 'TL'
		ELSE 'Lainnya'
		END AS Disposisi2_Status
	,a.Disposisi2_Notes
	,Disposisi3_By
	,DATE_FORMAT(a.Disposisi3_Date, '%d-%m-%Y') AS Disposisi3_Date
	,CASE a.Disposisi3_Status
		WHEN '01'
			THEN 'TL'
		ELSE 'Lainnya'
		END AS Disposisi3_Status
	,a.Disposisi3_Notes
FROM kasus a
LEFT JOIN tbl_satker b ON a.SatuanKerja = b.KODE_SATUAN_KERJA
LEFT JOIN tbl_user c ON c.UserID = a.konseptor$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_SuratMasuk_Filter_Sel` (IN `iStatus` INT, IN `DateFrom` DATE, IN `DateTo` DATE)   IF iStatus = 0 THEN
SELECT a.id
	,IFNULL(a.nomor_agenda,'') AS nomor_agenda
	,IFNULL(a.nomor_surat,'') AS nomor_surat
	,IFNULL(a.asal_surat,'') AS asal_surat
	,DATE_FORMAT(a.tanggal_surat, '%d-%m-%Y') AS tanggal_surat
	,IFNULL(a.perihal,'') AS perihal
	,IFNULL(a.LampiranSurat1,'') AS LampiranSurat1
	,IFNULL(a.LampiranSurat2,'') AS LampiranSurat2
	,IFNULL(a.LampiranSurat3,'') AS LampiranSurat3
	,IFNULL(a.LampiranSurat4,'') AS LampiranSurat4
	,IFNULL(a.LampiranSurat5,'') AS LampiranSurat5
	,IFNULL(a.LampiranSurat6,'') AS LampiranSurat6
	,IFNULL(a.Lampiran_LHA,'') AS Lampiran_LHA
	,CASE a.Dokumen_Yang_Akan_Dibuat
		WHEN '01'
			THEN 'Telaah'
		ELSE 'SK'
		END AS Dokumen_Yang_Akan_Dibuat
	,IFNULL(a.konseptor,'') AS UserID
	,IFNULL(c.FullName,'') AS konseptor
	,IFNULL(a.SatuanKerja,'') AS KODE_SATUAN_KERJA
	,IFNULL(b.SATUAN_KERJA,'') AS SatuanKerja
	,DATE_FORMAT(a.created_date, '%d-%m-%Y') AS created_date
	,IFNULL(a.create_user,'') AS create_user
	,DATE_FORMAT(a.update_date, '%d-%m-%Y') AS update_date
	,a.update_user
	,IFNULL(a.STATUS,0) STATUS
	,DATE_FORMAT(a.Disposisi1_Date, '%d-%m-%Y') AS Disposisi1_Date
	,a.Disposisi1_By
	,CASE a.Disposisi1_Status
		WHEN '01'
			THEN 'TL'
		ELSE 'Lainnya'
		END Disposisi1_Status
	,a.Disposisi1_Notes
	,Disposisi2_By
	,DATE_FORMAT(a.Disposisi2_Date, '%d-%m-%Y') AS Disposisi2_Date
	,CASE a.Disposisi2_Status
		WHEN '01'
			THEN 'TL'
		ELSE 'Lainnya'
		END AS Disposisi2_Status
	,a.Disposisi2_Notes
	,Disposisi3_By
	,DATE_FORMAT(a.Disposisi3_Date, '%d-%m-%Y') AS Disposisi3_Date
	,CASE a.Disposisi3_Status
		WHEN '01'
			THEN 'TL'
		ELSE 'Lainnya'
		END AS Disposisi3_Status
	,a.Disposisi3_Notes
FROM kasus a
LEFT JOIN tbl_satker b ON a.SatuanKerja = b.KODE_SATUAN_KERJA
LEFT JOIN tbl_user c ON c.UserID = a.konseptor
WHERE IFNULL(a.STATUS, 0) = 0 and a.tanggal_surat between DateFrom and DateTo;
ELSE
SELECT a.id
	,IFNULL(a.nomor_agenda,'') AS nomor_agenda
	,IFNULL(a.nomor_surat,'') AS nomor_surat
	,IFNULL(a.asal_surat,'') AS asal_surat
	,DATE_FORMAT(a.tanggal_surat, '%d-%m-%Y') AS tanggal_surat
	,IFNULL(a.perihal,'') AS perihal
	,IFNULL(a.LampiranSurat1,'') AS LampiranSurat1
	,IFNULL(a.LampiranSurat2,'') AS LampiranSurat2
	,IFNULL(a.LampiranSurat3,'') AS LampiranSurat3
	,IFNULL(a.LampiranSurat4,'') AS LampiranSurat4
	,IFNULL(a.LampiranSurat5,'') AS LampiranSurat5
	,IFNULL(a.LampiranSurat6,'') AS LampiranSurat6
	,IFNULL(a.Lampiran_LHA,'') AS Lampiran_LHA
	,CASE a.Dokumen_Yang_Akan_Dibuat
		WHEN '01'
			THEN 'Telaah'
		ELSE 'SK'
		END AS Dokumen_Yang_Akan_Dibuat
	,IFNULL(a.konseptor,'') AS UserID
	,IFNULL(c.FullName,'') AS konseptor
	,IFNULL(a.SatuanKerja,'') AS KODE_SATUAN_KERJA
	,IFNULL(b.SATUAN_KERJA,'') AS SatuanKerja
	,DATE_FORMAT(a.created_date, '%d-%m-%Y') AS created_date
	,IFNULL(a.create_user,'') AS create_user
	,DATE_FORMAT(a.update_date, '%d-%m-%Y') AS update_date
	,a.update_user
	,IFNULL(a.STATUS,0) STATUS
	,DATE_FORMAT(a.Disposisi1_Date, '%d-%m-%Y') AS Disposisi1_Date
	,a.Disposisi1_By
	,CASE a.Disposisi1_Status
		WHEN '01'
			THEN 'TL'
		ELSE 'Lainnya'
		END Disposisi1_Status
	,a.Disposisi1_Notes
	,Disposisi2_By
	,DATE_FORMAT(a.Disposisi2_Date, '%d-%m-%Y') AS Disposisi2_Date
	,CASE a.Disposisi2_Status
		WHEN '01'
			THEN 'TL'
		ELSE 'Lainnya'
		END AS Disposisi2_Status
	,a.Disposisi2_Notes
	,Disposisi3_By
	,DATE_FORMAT(a.Disposisi3_Date, '%d-%m-%Y') AS Disposisi3_Date
	,CASE a.Disposisi3_Status
		WHEN '01'
			THEN 'TL'
		ELSE 'Lainnya'
		END AS Disposisi3_Status
	,a.Disposisi3_Notes
FROM kasus a
LEFT JOIN tbl_satker b ON a.SatuanKerja = b.KODE_SATUAN_KERJA
LEFT JOIN tbl_user c ON c.UserID = a.konseptor
WHERE IFNULL(a.STATUS, 0) != 0 and a.tanggal_surat between DateFrom and DateTo;
END IF$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_SuratMasuk_GetKonseptor` (IN `iKODE_SATUAN_KERJA` INT)   SELECT konseptor FROM tbl_konseptor_satker
WHERE KODE_SATUAN_KERJA = iKODE_SATUAN_KERJA$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_SuratMasuk_Ins` (IN `inomor_agenda` VARCHAR(25), IN `inomor_surat` VARCHAR(100), IN `iasal_surat` VARCHAR(255), IN `itanggal_surat` DATETIME, IN `iperihal` VARCHAR(255), IN `iLampiranSurat1` VARCHAR(255), IN `iLampiranSurat2` VARCHAR(255), IN `iLampiranSurat3` VARCHAR(255), IN `iLampiranSurat4` VARCHAR(255), IN `iLampiranSurat5` VARCHAR(255), IN `iLampiranSurat6` VARCHAR(255), IN `iSatuanKerja` VARCHAR(255), IN `icreate_user` VARCHAR(25), IN `iLampiran_LHA` VARCHAR(255))   INSERT INTO kasus
(nomor_agenda,nomor_surat,asal_surat,tanggal_surat,
perihal,LampiranSurat1,LampiranSurat2,LampiranSurat3,
LampiranSurat4,LampiranSurat5,LampiranSurat6,
SatuanKerja,created_date,create_user,Status,Lampiran_LHA)
VALUES(
inomor_agenda,inomor_surat,iasal_surat,itanggal_surat,
iperihal,iLampiranSurat1,iLampiranSurat2,iLampiranSurat3,
iLampiranSurat4,iLampiranSurat5,iLampiranSurat6,
iSatuanKerja,CURRENT_TIMESTAMP,icreate_user,0,iLampiran_LHA)$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_SuratMasuk_Proses` (IN `iid` INT, IN `iupdate_user` VARCHAR(25))   UPDATE kasus
SET 
update_date = CURRENT_TIMESTAMP,
update_user = iupdate_user,
Status = 1
WHERE id = iid$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_SuratMasuk_Sel` ()   SELECT a.id
	,IFNULL(a.nomor_agenda,'') AS nomor_agenda
	,IFNULL(a.nomor_surat,'') AS nomor_surat
	,IFNULL(a.asal_surat,'') AS asal_surat
	,DATE_FORMAT(a.tanggal_surat, '%d-%m-%Y') AS tanggal_surat
	,IFNULL(a.perihal,'') AS perihal
	,IFNULL(a.LampiranSurat1,'') AS LampiranSurat1
	,IFNULL(a.LampiranSurat2,'') AS LampiranSurat2
	,IFNULL(a.LampiranSurat3,'') AS LampiranSurat3
	,IFNULL(a.LampiranSurat4,'') AS LampiranSurat4
	,IFNULL(a.LampiranSurat5,'') AS LampiranSurat5
	,IFNULL(a.LampiranSurat6,'') AS LampiranSurat6
	,IFNULL(a.Lampiran_LHA,'') AS Lampiran_LHA
	,CASE a.Dokumen_Yang_Akan_Dibuat
		WHEN '01'
			THEN 'Telaah'
		ELSE 'SK'
		END AS Dokumen_Yang_Akan_Dibuat
	,IFNULL(a.konseptor,'') AS UserID
	,IFNULL(c.FullName,'') AS konseptor
	,IFNULL(a.SatuanKerja,'') AS KODE_SATUAN_KERJA
	,IFNULL(b.SATUAN_KERJA,'') AS SatuanKerja
	,DATE_FORMAT(a.created_date, '%d-%m-%Y') AS created_date
	,IFNULL(a.create_user,'') AS create_user
	,DATE_FORMAT(a.update_date, '%d-%m-%Y') AS update_date
	,a.update_user
	,IFNULL(a.STATUS,0) STATUS
	,DATE_FORMAT(a.Disposisi1_Date, '%d-%m-%Y') AS Disposisi1_Date
	,a.Disposisi1_By
	,CASE a.Disposisi1_Status
		WHEN '01'
			THEN 'TL'
		ELSE 'Lainnya'
		END Disposisi1_Status
	,a.Disposisi1_Notes
	,Disposisi2_By
	,DATE_FORMAT(a.Disposisi2_Date, '%d-%m-%Y') AS Disposisi2_Date
	,CASE a.Disposisi2_Status
		WHEN '01'
			THEN 'TL'
		ELSE 'Lainnya'
		END AS Disposisi2_Status
	,a.Disposisi2_Notes
	,Disposisi3_By
	,DATE_FORMAT(a.Disposisi3_Date, '%d-%m-%Y') AS Disposisi3_Date
	,CASE a.Disposisi3_Status
		WHEN '01'
			THEN 'TL'
		ELSE 'Lainnya'
		END AS Disposisi3_Status
	,a.Disposisi3_Notes
FROM kasus a
LEFT JOIN tbl_satker b ON a.SatuanKerja = b.KODE_SATUAN_KERJA
LEFT JOIN tbl_user c ON c.UserID = a.konseptor
WHERE IFNULL(a.STATUS, 0) = 0$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_SuratMasuk_Upd` (IN `iid` INT, IN `inomor_agenda` VARCHAR(25), IN `inomor_surat` VARCHAR(100), IN `iasal_surat` VARCHAR(255), IN `itanggal_surat` DATETIME, IN `iperihal` VARCHAR(255), IN `iLampiranSurat1` VARCHAR(255), IN `iLampiranSurat2` VARCHAR(255), IN `iLampiranSurat3` VARCHAR(255), IN `iLampiranSurat4` VARCHAR(255), IN `iLampiranSurat5` VARCHAR(255), IN `iLampiranSurat6` VARCHAR(255), IN `iSatuanKerja` VARCHAR(255), IN `iupdate_user` VARCHAR(25), IN `iLampiran_LHA` VARCHAR(255))   UPDATE kasus
SET 
nomor_agenda = inomor_agenda,
nomor_surat = inomor_surat,
asal_surat = iasal_surat,
tanggal_surat = itanggal_surat,
perihal = iperihal,
LampiranSurat1 = case iLampiranSurat1 when '' then LampiranSurat1 else iLampiranSurat1 END ,
LampiranSurat2 = case iLampiranSurat2 when '' then LampiranSurat2 else iLampiranSurat2  END ,
LampiranSurat3 = case iLampiranSurat3 when '' then LampiranSurat3  else iLampiranSurat3 END ,
LampiranSurat4 = case iLampiranSurat4 when '' then LampiranSurat4  else iLampiranSurat4 END ,
LampiranSurat5 = case iLampiranSurat5 when '' then LampiranSurat5  else iLampiranSurat5 END ,
LampiranSurat6 = case iLampiranSurat6 when '' then LampiranSurat6  else iLampiranSurat6 END ,
Lampiran_LHA = case iLampiran_LHA when '' then Lampiran_LHA  else iLampiran_LHA END ,
SatuanKerja = iSatuanKerja,
update_date = CURRENT_TIMESTAMP,
update_user = iupdate_user,
Status = 0
WHERE id = iid$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_TelaahNo_GetNo` (IN `iBulan` INT, IN `iTahun` INT)   SELECT IFNULL(MAX(TelaahNo),0)+1 AS TelaahNo, YEAR(CURRENT_DATE), MONTH(CURRENT_DATE) FROM `tbl_notelaah` WHERE Bulan = iBulan AND Tahun = iTahun$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_TelaahNo_Insert` (IN `iTelaahNo` INT, IN `iBulan` INT, IN `iTahun` INT)   INSERT INTO tbl_notelaah (`TelaahNo`,`Bulan`,`Tahun`)
VALUES
(iTelaahNo,iBulan,iTahun)$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_Telaah_Create` (IN `iUserID` INT)   SELECT a.id
	,a.nomor_agenda
	,a.nomor_surat
	,a.asal_surat
	,DATE_FORMAT(a.tanggal_surat, '%d-%m-%Y') AS tanggal_surat
	,a.perihal
	,a.LampiranSurat1
	,a.LampiranSurat2
	,a.LampiranSurat3
	,a.LampiranSurat4
	,a.LampiranSurat5
	,a.LampiranSurat6
	,a.Lampiran_LHA
	,a.konseptor UserID
	,c.FullName konseptor
	,a.SatuanKerja AS KODE_SATUAN_KERJA
	,b.SATUAN_KERJA AS SatuanKerja
	,DATE_FORMAT(a.created_date, '%d-%m-%Y') AS created_date
	,a.create_user
	,DATE_FORMAT(a.update_date, '%d-%m-%Y') AS update_date
	,a.update_user
	,a.STATUS
	,DATE_FORMAT(a.Disposisi1_Date, '%d-%m-%Y') AS Disposisi1_Date
	,a.Disposisi1_By
	,a.Disposisi1_Status
	,a.Disposisi1_Notes
	,Disposisi2_By
	,DATE_FORMAT(a.Disposisi2_Date, '%d-%m-%Y') AS Disposisi2_Date
	,a.Disposisi2_Status
	,a.Disposisi2_Notes
	,Disposisi3_By
	,DATE_FORMAT(a.Disposisi3_Date, '%d-%m-%Y') AS Disposisi3_Date
	,a.Disposisi3_Status
	,a.Disposisi3_Notes
FROM kasus a
LEFT JOIN tbl_satker b ON a.SatuanKerja = b.KODE_SATUAN_KERJA
LEFT JOIN tbl_user c ON c.UserID = a.konseptor
WHERE STATUS = 4 and a.konseptor = iUserID$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_Telaah_Del` (IN `iid` INT, IN `iNIP` VARCHAR(25))   DELETE FROM tbl_telaah
WHERE ID = iid 
AND NIP = iNIP$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_Telaah_Ins` (IN `iID` INT, IN `iNIP` VARCHAR(25), IN `iDasar_dan_Bukti_Penunjang` TEXT, IN `iPelanggaran` TEXT, IN `iPasal_Pelanggaran` TEXT, IN `iRekomendasi_Hukuman` TEXT, IN `iAnalisa_dan_Pertimbangan` TEXT, IN `iKeputusan_Sidang_DPK` TEXT, IN `iCreated_User` VARCHAR(25), IN `iFullname` VARCHAR(255), IN `iTelaahNo` VARCHAR(100))   INSERT INTO tbl_telaah
(ID,NIP,Dasar_dan_Bukti_Penunjang,Pelanggaran,
 Pasal_Pelanggaran,Rekomendasi_Hukuman,
 Analisa_dan_Pertimbangan,Keputusan_Sidang_DPK,
 Created_User,Created_Date,Fullname, TelaahNo)
 VALUES
 (iID,iNIP,iDasar_dan_Bukti_Penunjang,iPelanggaran,
iPasal_Pelanggaran,iRekomendasi_Hukuman,iAnalisa_dan_Pertimbangan,
iKeputusan_Sidang_DPK,iCreated_User,CURRENT_TIMESTAMP,iFullname,iTelaahNo)$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_Telaah_Sel` (IN `iID` INT)   SELECT 
a.ID,
a.NIP,
a.Dasar_dan_Bukti_Penunjang,
a.Pelanggaran,
a.Pasal_Pelanggaran,
a.Rekomendasi_Hukuman,
IFNULL(a.Analisa_dan_Pertimbangan,'') AS Analisa_dan_Pertimbangan,
IFNULL(a.Keputusan_Sidang_DPK,'') AS Keputusan_Sidang_DPK,
a.Created_User,
DATE_FORMAT(a.Created_Date,'%d-%m-%Y') Created_Date,
a.Updated_User,
DATE_FORMAT(a.Updated_Date,'%d-%m-%Y') AS Updated_Date,
IFNULL(Fullname,'') AS Fullname,
c.SATUAN_KERJA as Satker,
IFNULL(A.Proses,0) as StatusProses,
IFNULL(A.FileTelaah,'') AS FileTelaah
FROM tbl_telaah a
LEFT JOIN kasus b on a.ID = b.id
left join tbl_satker c on b.SatuanKerja = c.KODE_SATUAN_KERJA
WHERE A.ID = iID$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_Telaah_Sel_ByIDNIP` (IN `iID` INT, IN `iNIP` VARCHAR(18))   SELECT 
a.ID,
a.NIP,
a.Dasar_dan_Bukti_Penunjang,
a.Pelanggaran,
a.Pasal_Pelanggaran,
a.Rekomendasi_Hukuman,
IFNULL(a.Analisa_dan_Pertimbangan,'') AS Analisa_dan_Pertimbangan,
IFNULL(a.Keputusan_Sidang_DPK,'') AS Keputusan_Sidang_DPK,
a.Created_User,
DATE_FORMAT(a.Created_Date,'%d-%m-%Y') Created_Date,
a.Updated_User,
DATE_FORMAT(a.Updated_Date,'%d-%m-%Y') AS Updated_Date,
IFNULL(a.FileTelaah,'') FileTelaah,IFNULL(Fullname,'') AS Fullname,
c.SATUAN_KERJA as Satker
FROM tbl_telaah a
LEFT JOIN kasus b on a.ID = b.id
left join tbl_satker c on b.SatuanKerja = c.KODE_SATUAN_KERJA
WHERE A.ID = iID AND A.NIP = iNIP$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_Telaah_SM_Sel` ()   SELECT a.id
	,IFNULL(a.nomor_agenda,'') AS nomor_agenda
	,IFNULL(a.nomor_surat,'') AS nomor_surat
	,IFNULL(a.asal_surat,'') AS asal_surat
	,DATE_FORMAT(a.tanggal_surat, '%d-%m-%Y') AS tanggal_surat
	,IFNULL(a.perihal,'') AS perihal
	,IFNULL(a.LampiranSurat1,'') AS LampiranSurat1
	,IFNULL(a.LampiranSurat2,'') AS LampiranSurat2
	,IFNULL(a.LampiranSurat3,'') AS LampiranSurat3
	,IFNULL(a.LampiranSurat4,'') AS LampiranSurat4
	,IFNULL(a.LampiranSurat5,'') AS LampiranSurat5
	,IFNULL(a.LampiranSurat6,'') AS LampiranSurat6
	,IFNULL(a.Lampiran_LHA,'') AS Lampiran_LHA
	,CASE a.Dokumen_Yang_Akan_Dibuat
		WHEN '01'
			THEN 'Telaah'
		ELSE 'SK'
		END AS Dokumen_Yang_Akan_Dibuat
	,IFNULL(a.konseptor,'') AS UserID
	,IFNULL(c.FullName,'') AS konseptor
	,IFNULL(a.SatuanKerja,'') AS KODE_SATUAN_KERJA
	,IFNULL(b.SATUAN_KERJA,'') AS SatuanKerja
	,DATE_FORMAT(a.created_date, '%d-%m-%Y') AS created_date
	,IFNULL(a.create_user,'') AS create_user
	,DATE_FORMAT(a.update_date, '%d-%m-%Y') AS update_date
	,a.update_user
	,IFNULL(a.STATUS,0) STATUS
	,DATE_FORMAT(a.Disposisi1_Date, '%d-%m-%Y') AS Disposisi1_Date
	,a.Disposisi1_By
	,CASE a.Disposisi1_Status
		WHEN '01'
			THEN 'TL'
		ELSE 'Lainnya'
		END Disposisi1_Status
	,a.Disposisi1_Notes
	,Disposisi2_By
	,DATE_FORMAT(a.Disposisi2_Date, '%d-%m-%Y') AS Disposisi2_Date
	,CASE a.Disposisi2_Status
		WHEN '01'
			THEN 'TL'
		ELSE 'Lainnya'
		END AS Disposisi2_Status
	,a.Disposisi2_Notes
	,Disposisi3_By
	,DATE_FORMAT(a.Disposisi3_Date, '%d-%m-%Y') AS Disposisi3_Date
	,CASE a.Disposisi3_Status
		WHEN '01'
			THEN 'TL'
		ELSE 'Lainnya'
		END AS Disposisi3_Status
	,a.Disposisi3_Notes
FROM kasus a
LEFT JOIN tbl_satker b ON a.SatuanKerja = b.KODE_SATUAN_KERJA
LEFT JOIN tbl_user c ON c.UserID = a.konseptor$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_Telaah_Upd` (IN `iID` INT, IN `iNIP` VARCHAR(25), IN `iDasar_dan_Bukti_Penunjang` TEXT, IN `iPelanggaran` TEXT, IN `iPasal_Pelanggaran` TEXT, IN `iRekomendasi_Hukuman` TEXT, IN `iAnalisa_dan_Pertimbangan` TEXT, IN `iKeputusan_Sidang_DPK` TEXT, IN `iCreated_User` VARCHAR(25), IN `iFileTelaah` TEXT, IN `iFullname` VARCHAR(255), IN `iTelaahNo` VARCHAR(100))   UPDATE tbl_telaah
SET 
Dasar_dan_Bukti_Penunjang = iDasar_dan_Bukti_Penunjang,
Pelanggaran = iPelanggaran,
Pasal_Pelanggaran = iPasal_Pelanggaran,
Rekomendasi_Hukuman = iRekomendasi_Hukuman,
Analisa_dan_Pertimbangan = iAnalisa_dan_Pertimbangan,
Keputusan_Sidang_DPK = iKeputusan_Sidang_DPK,
Updated_User = iCreated_User,
Updated_Date = CURRENT_TIMESTAMP,
FileTelaah = iFileTelaah,
Fullname = iFullname,
TelaahNo = iTelaahNo
WHERE
ID = iID
AND NIP = iNIP$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_Telaah_UpdStatus` (IN `iID` INT, IN `iNIP` VARCHAR(18), IN `iCreated_User` VARCHAR(25))   UPDATE KASUS SET STATUS = 5, update_user = iCreated_User,
update_date = CURRENT_TIMESTAMP
WHERE ID = iID$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_Telaah_UpdTelaahStatus` (IN `iID` INT, IN `iNIP` VARCHAR(18), IN `iCreated_User` VARCHAR(25))   UPDATE tbl_telaah set Proses = 1, Updated_User = iCreated_User, Created_Date = CURRENT_TIMESTAMP
where ID = iID AND NIP = iNIP$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_UserGroup_AlreadyExist` (IN `iGroupID` VARCHAR(25))   SELECT * FROM tbl_usergroup WHERE GroupID = iGroupID$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_UserGroup_Del` (IN `iGroupID` VARCHAR(25))   DELETE FROM tbl_usergroup
WHERE GroupID = iGroupID$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_UserGroup_Ins` (IN `iGroupID` VARCHAR(25), IN `iGroupDesc` VARCHAR(25), IN `iUserLogin` VARCHAR(25))   INSERT INTO tbl_usergroup (GroupID, GroupDesc, LastUser, LastUpdate)
VALUES (iGroupID, iGroupDesc, iUserLogin, CURRENT_TIMESTAMP())$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_UserGroup_Sel` ()   SELECT GroupID, GroupDesc, DATE_FORMAT(LastUpdate,'%d-%m-%Y') as LastUpdate, LastUser FROM tbl_usergroup$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_UserGroup_Upd` (IN `iGroupID` VARCHAR(25), IN `iGroupDesc` VARCHAR(25), IN `iLastUser` VARCHAR(25))   UPDATE tbl_usergroup SET GroupDesc = iGroupDesc, LastUser = iLastUser, LastUpdate = CURRENT_TIMESTAMP
WHERE GroupID = iGroupID$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_UserLogin_Upd_LastLogin` (IN `iNIP` VARCHAR(25))   UPDATE tbl_user
SET LASTLOGIN = CURRENT_TIMESTAMP
WHERE NIP = iNIP$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_User_AlreadyExist` (IN `iNIP` VARCHAR(25))   SELECT * FROM tbl_user WHERE NIP = iNIP$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_User_AlreadyUse` (IN `iUserID` INT)   SELECT * FROM tbl_konseptor_satker
WHERE KONSEPTOR = iUserID$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_User_byuser_sel` (IN `iNIP` VARCHAR(25))   BEGIN
	SELECT *  FROM tbl_user
    WHERE NIP = iNIP;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_User_Del` (IN `iUserId` INT, IN `iNIP` VARCHAR(18))   DELETE FROM tbl_user
WHERE USERID = iUserId
and NIP = iNIP$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_User_Ins` (IN `iUserName` VARCHAR(25), IN `iFullname` VARCHAR(255), IN `iPassword` VARCHAR(25), IN `iStatusAdmin` VARCHAR(1), IN `iGroupID` VARCHAR(25), IN `iLastUser` VARCHAR(25), IN `iNIP` VARCHAR(25), IN `iSatker` TEXT)   INSERT INTO tbl_user (UserName,Fullname,Password,StatusAdmin,GroupID,LastUser, LastUpdate, NIP, Satker)
VALUES
(iUserName,iFullname,iPassword,iStatusAdmin,iGroupID,iLastUser, CURRENT_TIMESTAMP,iNIP,iSatker)$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_User_sel` ()   SELECT 
UserID,
NIP,
UserName,
FullName,
Password,
B.GroupDesc as GroupDesc,
a.GroupID,
StatusAdmin,
A.LastUser,
A.LastLogin as LastLogin,
DATE_FORMAT(A.LastUpdate,'%d-%m-%Y') as LastUpdate,
Satker
FROM tbl_user a
LEFT JOIN tbl_usergroup b ON a.GroupID = b.GroupID$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_User_Upd` (IN `iUserID` INT, IN `iUserName` VARCHAR(25), IN `iPassword` VARCHAR(25), IN `iFullname` VARCHAR(255), IN `iStatusAdmin` VARCHAR(1), IN `iGroupID` VARCHAR(25), IN `iLastUser` VARCHAR(255), IN `iNIP` VARCHAR(25), IN `iSatker` TEXT)   UPDATE tbl_user
SET
UserName = iUserName,
Password = iPassword,
StatusAdmin = iStatusAdmin,
GroupID = iGroupID,
LastUser = iLastUser,
LastUpdate = CURRENT_TIMESTAMP,
Satker = iSatker
WHERE UserID = iUserID and NIP = iNIP$$

DELIMITER ;

-- --------------------------------------------------------

--
-- Table structure for table `hukdis`
--

CREATE TABLE `hukdis` (
  `id` int(3) NOT NULL,
  `hukuman` text DEFAULT NULL,
  `tingkat` varchar(10) DEFAULT NULL,
  `HukdisID` varchar(11) NOT NULL,
  `LastUser` varchar(25) NOT NULL,
  `LastUpdate` datetime DEFAULT NULL
) ENGINE=MyISAM DEFAULT CHARSET=latin1 ROW_FORMAT=DYNAMIC;

--
-- Dumping data for table `hukdis`
--

INSERT INTO `hukdis` (`id`, `hukuman`, `tingkat`, `HukdisID`, `LastUser`, `LastUpdate`) VALUES
(1, 'Teguran lisan', NULL, '0', '', NULL),
(2, 'Teguran tertulis', NULL, '0', '', NULL),
(3, 'Pernyataan tidak puas secara tertulis', NULL, '0', '', NULL),
(4, 'Penundaan kenaikan gaji berkala selama 1 (satu) tahun', NULL, '0', '', NULL),
(5, 'Penundaan kenaikan pangkat selama 1 (satu) tahun', NULL, '0', '', NULL),
(6, 'Penurunan pangkat setingkat lebih rendah selama 1 (satu) tahun', NULL, '0', '', NULL),
(7, 'Penurunan pangkat setingkat lebih rendah selama 3 (tiga) tahun', NULL, '0', '', NULL),
(8, 'Pemindahan dalam rangka penurunan jabatan setingkat lebih rendah', NULL, '0', '', NULL),
(9, 'Pembebasan dari jabatan', NULL, '0', '', NULL),
(10, 'Pemberhentian dengan hormat tidak atas permintaan sendiri sebagai PNS', NULL, '0', '', NULL),
(11, 'Pemberhentian tidak dengan hormat sebagai Pegawai Negeri Sipil', NULL, '0', '', NULL),
(12, 'Hukuman disiplin sesuai ketentuan', NULL, '0', '', NULL),
(13, 'Pemberhentian sementara dari jabatan negeri', NULL, '0', '', NULL),
(14, 'Pembebasan Sementara ', NULL, '0', '', NULL),
(15, 'Pengaktifan Kembali Sebagai Pegawai Negeri Sipil', NULL, '0', '', NULL),
(16, 'Pensiun ', NULL, '0', '', NULL),
(17, 'Pemberhentian Dengan Hormat Atas Permintaan Sendiri Sebagai Pegawai Negeri Sipil  (APS)', NULL, '0', '', NULL),
(18, 'Dipindahkan/Dimutasikan ', NULL, '0', '', NULL);

-- --------------------------------------------------------

--
-- Table structure for table `kasus`
--

CREATE TABLE `kasus` (
  `id` int(11) NOT NULL,
  `nomor_agenda` varchar(25) DEFAULT NULL,
  `nomor_surat` varchar(100) NOT NULL,
  `asal_surat` varchar(255) DEFAULT NULL,
  `tanggal_surat` date DEFAULT NULL,
  `perihal` varchar(255) DEFAULT NULL,
  `LampiranSurat1` varchar(255) DEFAULT NULL,
  `LampiranSurat2` varchar(255) NOT NULL,
  `LampiranSurat3` varchar(255) NOT NULL,
  `LampiranSurat4` varchar(255) NOT NULL,
  `LampiranSurat5` varchar(255) NOT NULL,
  `LampiranSurat6` varchar(255) NOT NULL,
  `Lampiran_LHA` varchar(255) NOT NULL,
  `konseptor` varchar(255) DEFAULT NULL,
  `SatuanKerja` varchar(255) DEFAULT NULL,
  `created_date` datetime DEFAULT NULL,
  `create_user` varchar(25) NOT NULL,
  `update_date` datetime DEFAULT NULL,
  `update_user` varchar(25) NOT NULL,
  `Status` int(1) NOT NULL DEFAULT 1,
  `Disposisi1_Date` datetime DEFAULT NULL,
  `Disposisi1_By` varchar(50) NOT NULL,
  `Disposisi1_Status` varchar(25) NOT NULL,
  `Disposisi1_Notes` text NOT NULL,
  `Disposisi2_By` varchar(25) NOT NULL,
  `Disposisi2_Date` datetime DEFAULT NULL,
  `Disposisi2_Status` varchar(25) NOT NULL,
  `Disposisi2_Notes` text NOT NULL,
  `Disposisi3_By` varchar(25) DEFAULT NULL,
  `Disposisi3_Date` datetime DEFAULT NULL,
  `Disposisi3_Status` varchar(25) NOT NULL,
  `Disposisi3_Notes` text NOT NULL,
  `Dokumen_Yang_Akan_Dibuat` varchar(2) NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=latin1 ROW_FORMAT=DYNAMIC;

--
-- Dumping data for table `kasus`
--

INSERT INTO `kasus` (`id`, `nomor_agenda`, `nomor_surat`, `asal_surat`, `tanggal_surat`, `perihal`, `LampiranSurat1`, `LampiranSurat2`, `LampiranSurat3`, `LampiranSurat4`, `LampiranSurat5`, `LampiranSurat6`, `Lampiran_LHA`, `konseptor`, `SatuanKerja`, `created_date`, `create_user`, `update_date`, `update_user`, `Status`, `Disposisi1_Date`, `Disposisi1_By`, `Disposisi1_Status`, `Disposisi1_Notes`, `Disposisi2_By`, `Disposisi2_Date`, `Disposisi2_Status`, `Disposisi2_Notes`, `Disposisi3_By`, `Disposisi3_Date`, `Disposisi3_Status`, `Disposisi3_Notes`, `Dokumen_Yang_Akan_Dibuat`) VALUES
(1, '001', 'B-1897/Un.15/HK.01.2/08/2020', 'Rektor UIN Sulthan Thaha Saifuddin Jambi', '2022-05-04', 'Mohon Petunjuk tentang status PNS a.n. Drs. H.M. Thahir, M.H.I', '1_26042022_085410.txt', '2_26042022_085410.txt', '3_26042022_085410.txt', '4_26042022_085410.txt', '5_26042022_085410.txt', '', '', '19', '6', '2022-04-26 20:54:10', 'admin', '2022-05-10 11:08:00', 'admin', 5, '2022-04-27 13:40:38', 'admin', '01', 'harap di tindak lanjuti', 'admin', '2022-04-28 08:29:13', '02', 'Mohon cantumkan berkas yang belum lengkap', 'admin', '2022-04-28 08:40:57', '01', 'mohon dibuat telaahan', '01'),
(2, '002', 'R-903/IJ/PS.01.3/11/2020', 'Inspektur Jenderal Kementerian Agama', '2020-11-30', 'Laporan Hasil Audit Investigasi terkait Pelanggaran Disiplin oleh Sdr. Pradikto Yudihutomo, SE NIP 199009272019031007 Pegawai pada Inspektorat Jenderal Kementerian Agama', '1_04052022_060210.txt', '', '', '', '', '', '2_04052022_060210.txt', '51', '143', '2022-05-04 18:02:10', 'admin', '2022-05-12 14:34:27', 'HJ. AZIEZAH KEBAHYANG, S.', 4, '2022-05-12 09:01:42', 'admin', '01', 'Tindak Lanjut', 'SEPTIAN SAPUTRA, S.Kom', '2022-05-12 14:26:12', '01', 'a', 'HJ. AZIEZAH KEBAHYANG, S.', '2022-05-12 14:34:27', '01', 'as', '01'),
(3, '003', 'I/XXX/2019/2/R', 'Universitas Hindu Negeri (UHN) Gusti Bagus Sugrina Denpasar, Provinsi Bali', '2022-05-11', 'Laporan Hasil Audit Investigasi Dana Penelitian Pada Universitas Hindu Negeri (UHN) Gusti Bagus Sugrina Denpasar, Provinsi Bali', 'Surat Tugas Tanggal 11 s.d 13-05-2022 Hotel Orchardz Industri_11052022_030851.pdf', '', '', '', '', 'SURAT TUGAS SIDANG DPK TGL 25 April  2022_ok_11052022_030851.pdf', '', NULL, '146', '2022-05-11 15:08:51', 'admin', '2022-05-12 14:26:21', 'SEPTIAN SAPUTRA, S.Kom', 3, '2022-05-12 09:05:31', 'admin', '01', 'test', 'SEPTIAN SAPUTRA, S.Kom', '2022-05-12 14:26:21', '01', 'b', NULL, NULL, '', '', ''),
(4, '004', 'R-765/IJ/PS.01.3/10/2020', 'Inspektorat Jenderal', '2022-05-12', 'Laporan Hasil Audit Investigasi Lnjutan terkait Digaan Pelanggaran Dsiplin Pegawai di Lingkungan Kementerian Agama Prov. Sultra', 'Uji Kompetensi Jabatan Fungsional Statistisi dan Pranata Komputer Periode 2022_12052022_083344.pdf', '', '', '', '', '08052022_Surat Penugasan WFO-WFH Pasca Lebaran 1443 H_ds_12052022_083344.pdf', '', NULL, '22', '2022-05-12 08:33:44', 'admin', NULL, '', 0, NULL, '', '', '', '', NULL, '', '', NULL, NULL, '', '', '');

-- --------------------------------------------------------

--
-- Table structure for table `tblfiles`
--
-- Error reading structure for table app_hukdis.tblfiles: #1932 - Table 'app_hukdis.tblfiles' doesn't exist in engine
-- Error reading data for table app_hukdis.tblfiles: #1064 - You have an error in your SQL syntax; check the manual that corresponds to your MariaDB server version for the right syntax to use near 'FROM `app_hukdis`.`tblfiles`' at line 1

-- --------------------------------------------------------

--
-- Table structure for table `tbl_konseptor_satker`
--

CREATE TABLE `tbl_konseptor_satker` (
  `KODE_SATUAN_KERJA` int(11) NOT NULL,
  `Konseptor` int(11) NOT NULL,
  `LastUser` varchar(25) NOT NULL,
  `LastUpdate` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `tbl_konseptor_satker`
--

INSERT INTO `tbl_konseptor_satker` (`KODE_SATUAN_KERJA`, `Konseptor`, `LastUser`, `LastUpdate`) VALUES
(143, 51, 'HJ. AZIEZAH KEBAHYANG, S.', '2022-05-12 14:33:58');

-- --------------------------------------------------------

--
-- Table structure for table `tbl_log`
--

CREATE TABLE `tbl_log` (
  `ID` int(11) NOT NULL,
  `UserLogin` varchar(100) NOT NULL,
  `Keterangan` text NOT NULL,
  `Waktu` datetime NOT NULL DEFAULT current_timestamp(),
  `Form` varchar(25) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `tbl_notelaah`
--

CREATE TABLE `tbl_notelaah` (
  `TelaahNo` int(11) NOT NULL,
  `Bulan` int(11) NOT NULL,
  `Tahun` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `tbl_notelaah`
--

INSERT INTO `tbl_notelaah` (`TelaahNo`, `Bulan`, `Tahun`) VALUES
(1, 5, 2022),
(2, 5, 2022);

-- --------------------------------------------------------

--
-- Table structure for table `tbl_satker`
--

CREATE TABLE `tbl_satker` (
  `KODE_SATUAN_KERJA` int(11) NOT NULL,
  `SATUAN_KERJA` varchar(255) NOT NULL,
  `LastUser` varchar(25) NOT NULL,
  `LastUpdate` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `tbl_satker`
--

INSERT INTO `tbl_satker` (`KODE_SATUAN_KERJA`, `SATUAN_KERJA`, `LastUser`, `LastUpdate`) VALUES
(1, 'Kanwil Kemenag Prov. Aceh	', '', NULL),
(2, 'Kanwil Kemenag Prov. Sumatera Utara', '', NULL),
(3, 'Kanwil Kemenag Prov. Sumatera Barat', '', NULL),
(4, 'Kanwil Kemenag Prov. Riau', '', NULL),
(5, 'Kanwil Kemenag Prov. Kepulauan Riau', '', NULL),
(6, 'Kanwil Kemenag Prov. Jambi', '', NULL),
(7, 'Kanwil Kemenag Prov. Sumatera Selatan', '', NULL),
(8, 'Kanwil Kemenag Prov. Bengkulu', '', NULL),
(9, 'Kanwil Kemenag Prov. Lampung', '', NULL),
(10, 'Kanwil Kemenag Prov. Kep. Bangka Belitung', '', NULL),
(11, 'Kanwil Kemenag Prov. Banten', '', NULL),
(12, 'Kanwil Kemenag Prov. DKI Jakarta', '', NULL),
(13, 'Kanwil Kemenag Prov. Jawa Barat', '', NULL),
(14, 'Kanwil Kemenag Prov. Jawa Tengah', '', NULL),
(15, 'Kanwil Kemenag Prov. DI Yogyakarta', '', NULL),
(16, 'Kanwil Kemenag Prov. Jawa Timur', '', NULL),
(17, 'Kanwil Kemenag Prov. Kalimantan Barat', '', NULL),
(18, 'Kanwil Kemenag Prov. Kalimantan Tengah', '', NULL),
(19, 'Kanwil Kemenag Prov. Kalimantan Selatan', '', NULL),
(20, 'Kanwil Kemenag Prov. Kalimantan Timur', '', NULL),
(21, 'Kanwil Kemenag Prov. Kalimantan Utara', '', NULL),
(22, 'Kanwil Kemenag Prov. Sulawesi Utara', '', NULL),
(23, 'Kanwil Kemenag Prov. Gorontalo', '', NULL),
(24, 'Kanwil Kemenag Prov. Sulawesi Tengah', '', NULL),
(25, 'Kanwil Kemenag Prov. Sulawesi Tenggara', '', NULL),
(26, 'Kanwil Kemenag Prov. Sulawesi Barat', '', NULL),
(27, 'Kanwil Kemenag Prov. Sulawesi Selatan', '', NULL),
(28, 'Kanwil Kemenag Prov. Bali', '', NULL),
(29, 'Kanwil Kemenag Prov. Nusa Tenggara Barat', '', NULL),
(30, 'Kanwil Kemenag Prov. Nusa Tenggara Timur', '', NULL),
(31, 'Kanwil Kemenag Prov. Maluku', '', NULL),
(32, 'Kanwil Kemenag Prov. Maluku Utara', '', NULL),
(33, 'Kanwil Kemenag Prov. Papua', '', NULL),
(34, 'Kanwil Kemenag Prov. Papua Barat', '', NULL),
(35, 'UIN Ar-Raniry Banda Aceh', '', NULL),
(36, 'UIN Sultan Syarif Qasim Pekanbaru', '', NULL),
(37, 'UIN Syarif Hidayatullah Jakarta', '', NULL),
(38, 'UIN Sunan Gunung Djati Bandung', '', NULL),
(39, 'UIN Sunan Kalijaga Yogyakarta', '', NULL),
(40, 'UIN Sunan Ampel Surabaya', '', NULL),
(41, 'UIN Maulana Malik Ibrahim Malang', '', NULL),
(42, 'UIN Alauddin Makassar', '', NULL),
(43, 'UIN Sumatera Utara Medan', '', NULL),
(44, 'IAIN Padangsidempuan', '', NULL),
(45, 'UIN Imam Bonjol Padang', '', NULL),
(46, 'UIN Fatmawati Sukarno Bengkulu', '', NULL),
(47, 'UIN Sulthan Thaha Saifuddin Jambi', '', NULL),
(48, 'UIN Raden Fatah Palembang', '', NULL),
(49, 'UIN Raden Intan Bandar lampung', '', NULL),
(50, 'UIN Sultan Maulana Hasanuddin Banten', '', NULL),
(51, 'IAIN Syekh Nur Jati Cirebon', '', NULL),
(52, 'UIN Walisongo Semarang', '', NULL),
(53, 'UIN Raden Mas Said Surakarta', '', NULL),
(54, 'UIN Sayyid Ali Rahmatullah Tulungagung', '', NULL),
(55, 'UIN Antasari Banjarmasin', '', NULL),
(56, 'IAIN Sultan Amai Gorontalo', '', NULL),
(57, 'IAIN Ambon', '', NULL),
(58, 'UIN Mataram', '', NULL),
(59, 'IAIN Pontianak', '', NULL),
(60, 'UIN Datokarama Palu', '', NULL),
(61, 'IAIN Ternate', '', NULL),
(62, 'IAIN Bukittinggi', '', NULL),
(63, 'IAIN Lhokseumawe', '', NULL),
(64, 'IAIN Zawiyah Cot Kala Langsa', '', NULL),
(65, 'IAIN Gajah Putih Takengon', '', NULL),
(66, 'IAIN Batusangkar', '', NULL),
(67, 'IAIN Kerinci', '', NULL),
(68, 'IAIN Curup', '', NULL),
(69, 'IAIN Syaih Abdurrahman Siddiq Bangka Belitung', '', NULL),
(70, 'IAIN Metro', '', NULL),
(71, 'IAIN Kudus', '', NULL),
(72, 'IAIN Pekalongan', '', NULL),
(73, 'UIN Prof. KH. Saifuddin Zuhri Purwokerto', '', NULL),
(74, 'IAIN Salatiga', '', NULL),
(75, 'UIN KH. Achmad Siddiq Jember', '', NULL),
(76, 'IAIN Kediri', '', NULL),
(77, 'IAIN Madura', '', NULL),
(78, 'IAIN Ponorogo', '', NULL),
(79, 'UIN Sultan Aji Muhammad Idris Samarinda', '', NULL),
(80, 'IAIN Palangkaraya', '', NULL),
(81, 'IAIN Manado', '', NULL),
(82, 'IAIN Palopo', '', NULL),
(83, 'IAIN Pare-Pare', '', NULL),
(84, 'IAIN Bone', '', NULL),
(85, 'IAIN Kendari', '', NULL),
(86, 'IAIN Fattahul Muluk Papua', '', NULL),
(87, 'IAIN Sorong', '', NULL),
(88, 'IAKN Ambon', '', NULL),
(89, 'STAKN Palangkaraya', '', NULL),
(90, 'IAKN Tarutung', '', NULL),
(91, 'IAKN Toraja', '', NULL),
(92, 'STAKN Sentani', '', NULL),
(93, 'IAKN Manado', '', NULL),
(94, 'IAKN Kupang', '', NULL),
(95, 'IHDN DENPASAR', '', NULL),
(96, 'IAHN Tampung Penyang Palangkaraya', '', NULL),
(97, 'IAHN Gde Pudja Mataram', '', NULL),
(98, 'STABN Sriwijaya Tangerang', '', NULL),
(99, 'STABN Wonogiri', '', NULL),
(100, 'Balai Diklat Keagamaan Aceh', '', NULL),
(101, 'Balai Diklat Keagamaan Medan', '', NULL),
(102, 'Balai Diklat Keagamaan Padang', '', NULL),
(103, 'Balai Diklat Keagamaan Palembang', '', NULL),
(104, 'Balai Diklat Keagamaan Jakarta', '', NULL),
(105, 'Balai Diklat Keagamaan Bandung', '', NULL),
(106, 'Balai Diklat Keagamaan Semarang', '', NULL),
(107, 'Balai Diklat Keagamaan Surabaya', '', NULL),
(108, 'Balai Diklat Keagamaan Banjarmasin', '', NULL),
(109, 'Balai Diklat Keagamaan Manado', '', NULL),
(110, 'Balai Diklat Keagamaan Makassar', '', NULL),
(111, 'Balai Diklat Keagamaan Ambon', '', NULL),
(112, 'Balai Diklat Keagamaan Denpasar', '', NULL),
(113, 'Balai Litbang Agama Jakarta', '', NULL),
(114, 'Balai Litbang Agama Semarang', '', NULL),
(115, 'Balai Litbang Agama Makassar', '', NULL),
(116, 'STAHN Mpu Kuturan Singaraja Prov. Bali', '', NULL),
(117, 'STAIN Abdurrahman Prov. Kepulauan Riau', '', NULL),
(118, 'STAIN Bengkalis', '', NULL),
(119, 'STAIN Majene', '', NULL),
(120, 'STAIN Mandailing Natal', '', NULL),
(121, 'STAIN Teungku Dirundeng Meulaboh', '', NULL),
(122, 'Balai Diklat Keagamaan Papua', '', NULL),
(123, 'Asrama Haji Embarkasi Banda Aceh', '', NULL),
(124, 'Asrama Haji Embarkasi Medan', '', NULL),
(125, 'Asrama Haji Embarkasi Padang', '', NULL),
(126, 'Asrama Haji Embarkasi Jakarta', '', NULL),
(127, 'Asrama Haji Embarkasi Surabaya', '', NULL),
(128, 'Asrama Haji Embarkasi Banjarmasin', '', NULL),
(129, 'Asrama Haji Embarkasi Balikpapan', '', NULL),
(130, 'Asrama Haji Embarkasi Makassar', '', NULL),
(131, 'Asrama Haji Embarkasi Lombok', '', NULL),
(132, 'Asrama Haji Embarkasi Bekasi', '', NULL),
(133, 'STAKATN Pontianak', '', NULL),
(134, 'IAKN Palangkaraya', '', NULL),
(135, 'Sekretariat Jenderal', '', NULL),
(136, 'Ditjen Pendidikan Islam', '', NULL),
(137, 'Ditjen Bimbingan Masyarakat Islam', '', NULL),
(138, 'Ditjen Bimbingan Masyarakat Kristen', '', NULL),
(139, 'Ditjen Bimbingan Masyarakat Katolik', '', NULL),
(140, 'Ditjen Bimbingan Masyarakat Hindu', '', NULL),
(141, 'Ditjen Bimbingan Masyarakat Buddha', '', NULL),
(142, 'Ditjen Penyelenggaraan Haji dan Umrah', '', NULL),
(143, 'Inspektorat Jenderal', '', NULL),
(144, 'Badan Litbang Serta Pendidikan dan Pelatihan', '', NULL),
(145, 'Badan Penyelenggara Jaminan Produk Halal', '', NULL),
(146, 'UHN Bagus Sugriwa Denpasar', '', NULL);

-- --------------------------------------------------------

--
-- Table structure for table `tbl_telaah`
--

CREATE TABLE `tbl_telaah` (
  `ID` int(11) NOT NULL,
  `NIP` varchar(18) NOT NULL,
  `Fullname` varchar(255) NOT NULL,
  `Dasar_dan_Bukti_Penunjang` text NOT NULL,
  `Pelanggaran` text NOT NULL,
  `Pasal_Pelanggaran` text NOT NULL,
  `Rekomendasi_Hukuman` text NOT NULL,
  `Analisa_dan_Pertimbangan` text NOT NULL,
  `Keputusan_Sidang_DPK` text NOT NULL,
  `Created_User` varchar(25) NOT NULL,
  `Created_Date` datetime NOT NULL,
  `Updated_User` varchar(25) NOT NULL,
  `Updated_Date` datetime NOT NULL,
  `FileTelaah` varchar(255) NOT NULL,
  `Proses` int(1) NOT NULL,
  `TelaahNo` varchar(30) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `tbl_telaah`
--

INSERT INTO `tbl_telaah` (`ID`, `NIP`, `Fullname`, `Dasar_dan_Bukti_Penunjang`, `Pelanggaran`, `Pasal_Pelanggaran`, `Rekomendasi_Hukuman`, `Analisa_dan_Pertimbangan`, `Keputusan_Sidang_DPK`, `Created_User`, `Created_Date`, `Updated_User`, `Updated_Date`, `FileTelaah`, `Proses`, `TelaahNo`) VALUES
(1, '198103242002122001', 'ARNYDIANTY, S.sos', 'BBB', 'CCC', 'DDD', 'EEE', 'FFF', '', 'admin', '2022-05-10 11:08:07', 'admin', '2022-05-10 09:44:28', '6_10052022_094417.txt', 1, ''),
(2, '196905052000031002', 'Drs. MOHAMMAD FARID WADJDI, M.M.', 'Surat Inspektur Jenderal Kementerian Agama Nomor R-570/IJ/PS.01.3/07/2019 tanggal 16 Juni 2019 perihal Laporan Hasil Audit Tujuan Tertentu Atas Dugaan Pelanggaran Disiplin Pegawai pada Sekretariat Jenderal Kementerian Agama Jakarta Pusat; dan\nBerita Acara Pemeriksaan tanggal 7 Mei 2019 terhadap Sdr. Drs. Mohammad Farid Wadjdi, M.M. NIP. 196905052000031002.', 'Sdr Drs. Mohammad Farid Wadjdi, M.M. pada saat proses mutasi pegawai tidak sesuai kewenangannya dengan tidak memberikan nomor surat mutasi a.n Sdr Dono Santoso yang tidak dimintakan kepada unit kerja yang mempunyai tugas dan fungsi tata usaha. Penomoran pada Keputusan Menteri tersebut tidak terdaftar pada TU Biro Kepegawaian, yaitu surat Keputusan dibuat oleh Sdr. Maruli dan penomoran yang bersangkutan minta ke TU Biro Kepegawaian.\nYang bersangkutan pada proses gugat cerai tidak dilakukan sesuai dengan prosedur sebagaimana pertimbangan hakim pada Putusan Pengadilan Agama Cibinong Nomor 583/Pdt.G/PA.Cbn tanggal 5 Juli 2018 Pengadilan Agama Cibinong memutuskan permohonan gugatan Sdr. Farid Wadjdi (pemohon) tidak dapat diterima dan membebankan kepada Pemohon untuk membayar biaya perkara sejumlah Rp. 401.000,- (empat ratus satu ribu rupiah), karena kedua belah pihak tidak hadir saat dilakukan sidang proses mediasi oleh pengadilan.\nTidak memberikan nafkah lahir dan batin kepada istri yang bersangkutan a.n Sdr. Farida Ulfah sesuai Putusan Pengadilan Agama Cibinong Nomor 583/Pdt.G/PA.Cbn tanggal 5 Juli 2018 Pengadilan Agama Cibinong Putusan Pengadilan Agama Cibinong Nomor 1538/Pdt.G/2018/PA.Bgr tanggal 21 Februari 2019.\nYang bersangkutan menindaklanjuti upaya keberatan atas hukuman disiplin Pegawai Negeri Sipil yang tidak sesuai dengan ketentuan yaitu penjatuhan hukuman disiplin berupa Penurunan Pangkat Setingkat Lebih Rendah Selama 1 (satu) Tahun yang dijatuhkan oleh Pejabat Pembina Kepegawaian dalam hal ini Menteri Agama seharusnya tidak dapat diajukan upaya administratif, baik berupa keberatan atau banding aministratif antara lain SK Hukuman Disiplin a.n.\na. Sdr. Dr. I Nyoman Wijana, S.Sos., M.Si., M.Pd;\nb. Sdr Ida Ayu Nyoman Widia Laksmi, S.E., M.MI;\nc. Sdr. Lilik, S.Ag., M.Pd.H;\nd. Sdr. Pranata, S.Pd;\ne. Sdr. Ibelala Gea, S.Th., M.Si\n	\nkarena hukuman disiplin tersebut tidak diajukan upaya hukum melalui PTUN oleh Sdr. Dr. I Nyoman Wijana, S.Sos., M.Si., M.Pd dkk maka Hukuman disiplin dimaksud telah berkekuatan hukum  tetap, tetapi Sdr. Farid Wadjdi sebagai Kepala Bagian Pengadaan dan Pertimbangan Pegawai menerima permohonan upaya hukum dimaksud yang tidak memiliki dasar hukum untuk mendapat persetujuan untuk sidang Dewan Pertimbangan Kepegawaian (DPK).\nTidak ditindaklanjutinya hasil sidang DPK a.n. Sdr. Nasruddin dan Sdr Lale Lasmining Pudji Jagad dengan pembuatan SK hukuman disiplin yang tidak sesuai rekomendasi STL Inspektorat Jenderal merupakan arahan Sdr. Farid Wadjdi dan Sdr. Ahmadi meskipun dalam BAP Sdr. Farid Wadjdi mengakuinya sebagai sebuah kelalaian Sdr. Farid Wadjdi sebagai Kepala Bagian Pengadaan dan Pertimbangan Pegawai.\nAkibatnya:\na. Hukuman disiplin Sdr. Sdr. Dr. I Nyoman Wijana, S.Sos., M.Si., M.Pd tidak dilaksanakan;\nb.	Sdr. Sdr. Dr. I Nyoman Wijana, S.Sos., M.Si., M.Pd terpilih sebagai Ketua STHN Gede Pudja Mataram dan Sdr. Ida Ayu Nyoman Widia Laksmi terpilih sebagai Wakil II STHN Gede Pudja Mataram;\nc.	Sdr. Lilik, S.Ag., M.Pd.H;dan Sdr. Pranat, S.Pd jabatan Lektor STHN Tampung Penyang Palangkaraya tidak melaksanakan hukuman disiplin sebagaimana SK yang telah memiliki kekuatan hukum tetap;\nd.    Sdr. Nasruddin, S.Sos., M.Pd.I menjadi Kepala Kanwil Kemenag Prov. Nusa Tenggara Barat;\ne.	Sdr. Lale Laksmining Puji Jagad, S.Ag pengadministrsi  pada Seksi Pendidikan Madrasah Kankemenag Lombok Tengah tidak mendapatkan sanksi apapun;', 'Yang bersangkutan melanggar ketentuan Pasal 3 angka 4, angka 5, angka 10 dan Pasal 4 angka 1 dan angka Peraturan Pemerintah Nomor 53 tahun 2010 tentang Disiplin Pegawai Negeri Sipil.', 'Yang bersangkutan direkomendasikan oleh Inspektur Jenderal Kementerian Agama untuk dijatuhi hukuman disiplin berupa Penurunan Pangkat Setingkat Lebih Rendah Selama 1 (satu) Tahun sesuai ketentuan Pasal 7 ayat (3) huruf c Peraturan Pemerintah Nomor 53 Tahun 2010 dan dipindahkan dari Sekretariat Jenderal ke Satker yang lain.', 'Yang bersangkutan bersikap kooperatif.', '', 'HANI FEBRIA HANDAYANI, S.', '2022-05-13 15:46:06', '', '0000-00-00 00:00:00', '', 0, 'R-002/B.II/2-b/KP.04.1/05/2022');

-- --------------------------------------------------------

--
-- Table structure for table `tbl_user`
--

CREATE TABLE `tbl_user` (
  `UserID` int(11) NOT NULL,
  `NIP` varchar(18) NOT NULL,
  `UserName` varchar(25) DEFAULT NULL,
  `FullName` varchar(100) DEFAULT NULL,
  `Satker` text NOT NULL,
  `Password` varchar(25) NOT NULL,
  `GroupID` varchar(25) DEFAULT NULL,
  `StatusAdmin` int(11) NOT NULL,
  `LastUpdate` datetime DEFAULT NULL ON UPDATE current_timestamp(),
  `LastUser` varchar(25) DEFAULT NULL,
  `LastLogin` datetime DEFAULT NULL ON UPDATE current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `tbl_user`
--

INSERT INTO `tbl_user` (`UserID`, `NIP`, `UserName`, `FullName`, `Satker`, `Password`, `GroupID`, `StatusAdmin`, `LastUpdate`, `LastUser`, `LastLogin`) VALUES
(1, 'admin', NULL, 'administrator', '', 'u1GL+U7ZpwEEUboOXNIaLg==', '01', 0, '2022-05-12 19:04:24', 'admin', '2022-05-12 19:04:24'),
(42, '199512222022032002', NULL, 'DEBBY MUSTIKA WATY, S.T', 'Subbagian Pertimbangan Kepegawaian Bagian Pengadaan dan Pertimbangan Pegawai Biro Kepegawaian Sekretariat Jenderal Kementerian Agama', 'ufBCRgBg8ZF1wsCeZm7MEQ==', '01', 1, '2022-05-13 15:13:54', 'admin', '2022-05-13 15:13:54'),
(46, '198005022011011007', NULL, 'FIESTYO IMANTA SANTOSO, S', 'Subbagian Pertimbangan Kepegawaian Bagian Pengadaan dan Pertimbangan Pegawai Biro Kepegawaian Sekretariat Jenderal Kementerian Agama', 'iV/lMntjCj8kWG37E74tVA==', '02', 1, '2022-05-11 13:12:18', '199512222022032002', NULL),
(47, '197712242011012005', NULL, 'INDRIA SUKMAWATI., S.Pd', 'Subbagian Pertimbangan Kepegawaian Bagian Pengadaan dan Pertimbangan Pegawai Biro Kepegawaian Sekretariat Jenderal Kementerian Agama', 'x9bj44T0Om4ZLrTc1Hv4Ag==', '02', 0, '2022-05-11 13:12:32', '199512222022032002', NULL),
(48, '197808182009011017', NULL, 'KARNUDIN, S.E., MAB', 'Subbagian Pertimbangan Kepegawaian Bagian Pengadaan dan Pertimbangan Pegawai Biro Kepegawaian Sekretariat Jenderal Kementerian Agama', 'u22mSAaDik9nl/KF4T5yzg==', '02', 0, '2022-05-11 13:21:30', '199512222022032002', NULL),
(49, '196609181988031001', NULL, 'REKO BUDI UTOMO', 'Subbagian Pertimbangan Kepegawaian Bagian Pengadaan dan Pertimbangan Pegawai Biro Kepegawaian Sekretariat Jenderal Kementerian Agama', 'Cqh7p3/k9S0KHrMZHWo6UQ==', '02', 0, '2022-05-11 13:21:46', '199512222022032002', NULL),
(50, '198007022009011011', NULL, 'WIDI TANURJAYA, S.H', 'Subbagian Pertimbangan Kepegawaian Bagian Pengadaan dan Pertimbangan Pegawai Biro Kepegawaian Sekretariat Jenderal Kementerian Agama', 'QDLt+5oiMdTfxngan/+LFg==', '02', 0, '2022-05-11 13:24:26', '199512222022032002', NULL),
(51, '198302202003122001', NULL, 'HANI FEBRIA HANDAYANI, S.', 'Subbagian Tata Usaha Bagian Pengadaan dan Pertimbangan Pegawai Biro Kepegawaian Sekretariat Jenderal Kementerian Agama', '9Qw6TD1FeiVhohfO1tRYGQ==', '02', 0, '2022-05-13 15:40:56', '199512222022032002', '2022-05-13 15:40:56'),
(52, '196406022003122001', NULL, 'HJ. AZIEZAH KEBAHYANG, S.', 'Subbagian Pertimbangan Kepegawaian Bagian Pengadaan dan Pertimbangan Pegawai Biro Kepegawaian Sekretariat Jenderal Kementerian Agama', 'x7Iv4Ya6XA+XImXE5RIa/g==', '06', 0, '2022-05-12 14:32:30', '199512222022032002', '2022-05-12 14:32:30'),
(53, '198007202006041003', NULL, 'Dr. NURUDIN, S.Pd.I., M.S', 'Biro Kepegawaian Sekretariat Jenderal Kementerian Agama', 'HLhEGMdfQq8Ef97vniptUA==', '02', 0, '2022-05-11 13:26:08', '199512222022032002', NULL),
(55, '198609222011011014', NULL, 'SEPTIAN SAPUTRA, S.Kom', 'Bagian Pengadaan dan Pertimbangan Pegawai Biro Kepegawaian Sekretariat Jenderal Kementerian Agama', 'MzrGJTl2zT7REEIqgX5mOQ==', '05', 0, '2022-05-12 19:07:42', 'DEBBY MUSTIKA WATY, S.T', NULL),
(56, '196611131994031001', NULL, 'H. MOHAMAD ALI IRFAN, SE, MM, M.Ak', 'Sekretariat Inspektorat Jenderal Inspektorat Jenderal  Kementerian Agama', 'pOEfFPgnZM+CyPtn2racsA==', '03', 0, '2022-05-12 19:08:15', 'DEBBY MUSTIKA WATY, S.T', NULL),
(57, '196407191986032001', NULL, 'GIRI REKNOWATI, S.Pd', 'Subbagian Pertimbangan Kepegawaian Bagian Pengadaan dan Pertimbangan Pegawai Biro Kepegawaian Sekretariat Jenderal Kementerian Agama', 'KlZUxmWw4Ek9YPrAdxKg+w==', '02', 0, '2022-05-12 19:08:43', 'DEBBY MUSTIKA WATY, S.T', NULL),
(58, '197008302009011006', NULL, 'ALAMSYAH, S.E', 'Subbagian Pertimbangan Kepegawaian Bagian Pengadaan dan Pertimbangan Pegawai Biro Kepegawaian Sekretariat Jenderal Kementerian Agama', 'Qa40oMNT5NBSF+6ICQTlxQ==', '02', 0, '2022-05-12 19:09:21', 'DEBBY MUSTIKA WATY, S.T', NULL);

-- --------------------------------------------------------

--
-- Table structure for table `tbl_usergroup`
--

CREATE TABLE `tbl_usergroup` (
  `GroupID` varchar(10) NOT NULL,
  `GroupDesc` varchar(50) DEFAULT NULL,
  `LastUpdate` datetime DEFAULT NULL ON UPDATE current_timestamp(),
  `LastUser` varchar(25) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `tbl_usergroup`
--

INSERT INTO `tbl_usergroup` (`GroupID`, `GroupDesc`, `LastUpdate`, `LastUser`) VALUES
('01', 'Admin', '2022-04-21 07:19:05', 'admin'),
('02', 'Konseptor', '2022-04-21 07:19:13', 'admin'),
('03', 'Satuan Kerja', '2022-04-25 21:06:44', 'admin'),
('04', 'Kepala Biro', NULL, NULL),
('05', 'Koordinator', NULL, NULL),
('06', 'Sub Koordinator', '2022-05-11 13:02:30', '199512222022032002');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `hukdis`
--
ALTER TABLE `hukdis`
  ADD PRIMARY KEY (`id`) USING BTREE;

--
-- Indexes for table `kasus`
--
ALTER TABLE `kasus`
  ADD PRIMARY KEY (`id`) USING BTREE;

--
-- Indexes for table `tbl_konseptor_satker`
--
ALTER TABLE `tbl_konseptor_satker`
  ADD PRIMARY KEY (`KODE_SATUAN_KERJA`,`Konseptor`);

--
-- Indexes for table `tbl_log`
--
ALTER TABLE `tbl_log`
  ADD PRIMARY KEY (`ID`);

--
-- Indexes for table `tbl_satker`
--
ALTER TABLE `tbl_satker`
  ADD PRIMARY KEY (`KODE_SATUAN_KERJA`);

--
-- Indexes for table `tbl_telaah`
--
ALTER TABLE `tbl_telaah`
  ADD PRIMARY KEY (`ID`,`NIP`);

--
-- Indexes for table `tbl_user`
--
ALTER TABLE `tbl_user`
  ADD PRIMARY KEY (`UserID`,`NIP`);

--
-- Indexes for table `tbl_usergroup`
--
ALTER TABLE `tbl_usergroup`
  ADD PRIMARY KEY (`GroupID`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `hukdis`
--
ALTER TABLE `hukdis`
  MODIFY `id` int(3) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=22;

--
-- AUTO_INCREMENT for table `kasus`
--
ALTER TABLE `kasus`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT for table `tbl_konseptor_satker`
--
ALTER TABLE `tbl_konseptor_satker`
  MODIFY `KODE_SATUAN_KERJA` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=144;

--
-- AUTO_INCREMENT for table `tbl_log`
--
ALTER TABLE `tbl_log`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `tbl_satker`
--
ALTER TABLE `tbl_satker`
  MODIFY `KODE_SATUAN_KERJA` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=148;

--
-- AUTO_INCREMENT for table `tbl_user`
--
ALTER TABLE `tbl_user`
  MODIFY `UserID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=59;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
