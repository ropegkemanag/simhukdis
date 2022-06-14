-- phpMyAdmin SQL Dump
-- version 5.1.3
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Jun 14, 2022 at 08:24 AM
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
-- Database: `simhukdis`
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

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_Disposisi1_Upd` (IN `i_id` INT, IN `iStatus` INT(1), IN `iDisposisi_By` VARCHAR(100), IN `iDisposisi_Note` TEXT, IN `iDisposisi_Status` VARCHAR(25))   UPDATE kasus
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

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_Disposisi2_Upd` (IN `i_id` INT, IN `iStatus` INT(1), IN `iDisposisi_By` VARCHAR(100), IN `iDisposisi_Note` TEXT, IN `iDisposisi_Status` VARCHAR(25))   UPDATE kasus 
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

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_Disposisi3_Upd` (IN `i_id` INT, IN `iStatus` INT(1), IN `iDisposisi_By` VARCHAR(100), IN `iDisposisi_Note` TEXT, IN `iDisposisi_Status` VARCHAR(25), IN `iKonseptor` VARCHAR(25), IN `iDokumen_Yang_Akan_Dibuat` VARCHAR(2))   UPDATE kasus 
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

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_Disposisi_Upd` (IN `i_id` INT, IN `iStatus` INT(1), IN `iDisposisi_By` VARCHAR(100), IN `iDisposisi_Note` TEXT, IN `iKonseptor` VARCHAR(25))   UPDATE kasus
SET STATUS = iStatus,
Disposisi_By = iDisposisi_By,
Disposisi_Note = iDisposisi_Note,
Disposisi_Date = CURRENT_TIMESTAMP,
update_user = iDisposisi_By,
update_date = CURRENT_TIMESTAMP,
konseptor = iKonseptor
WHERE ID = i_id$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_DPK_Already_Sel` (IN `iid` INT, IN `iNIP` VARCHAR(18))   SELECT * FROM tbl_dpk WHERE ID = iid and NIP = iNIP$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_DPK_Ins` (IN `iid` INT, IN `iNIP` VARCHAR(18), IN `iCatatan_Sidang` TEXT, IN `iCreate_User` VARCHAR(100), IN `iTanggal_Sidang` DATE, IN `iKeputusanSidang` INT, IN `iDasar_Bukti` TEXT, IN `iPelanggaran` TEXT, IN `iPasal_Pelanggaran` INT, IN `iMengingat` TEXT)   INSERT INTO tbl_dpk (ID, NIP, Catatan_Sidang, Create_User, Create_Date, Tanggal_Sidang,KeputusanSidang, Dasar_Bukti, Pelanggaran, Pasal_Pelanggaran,Mengingat)
VALUES
(iid, iNIP, iCatatan_Sidang, iCreate_User, CURRENT_TIMESTAMP(),iTanggal_Sidang,iKeputusanSidang, iDasar_Bukti, iPelanggaran, iPasal_Pelanggaran,iMengingat)$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_DPK_Sel` ()   SELECT 
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
A.perihal,
c.Catatan,
d.KeputusanSidang as KeputusanSidang,
e.hukuman,
d.Catatan_Sidang,
DATE_FORMAT(c.tanggal_sidang, '%d-%m-%Y') AS tanggal_sidang_pra_dpk,
DATE_FORMAT(d.tanggal_sidang, '%d-%m-%Y') AS tanggal_sidang_dpk,
d.Mengingat
FROM kasus a 
LEFT JOIN tbl_telaah b	on a.id = b.ID
LEFT JOIN tbl_pradpk c on a.id = c.ID and b.NIP = c.NIP
LEFT JOIN tbl_dpk d on a.id = d.ID and b.NIP = d.NIP
left join hukdis e on e.id = d.KeputusanSidang
WHERE IFNULL(A.Status,0) = 6 and IFNULL(C.Status,0) = 1 and IFNULL(d.Status,0) = 0$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_DPK_Upd` (IN `iid` INT, IN `iNIP` VARCHAR(18), IN `iKeputusanSidang` INT, IN `iCreate_User` VARCHAR(100), IN `iTanggal_Sidang` DATE, IN `iCatatan_Sidang` TEXT, IN `iDasar_Bukti` TEXT, IN `iPelanggaran` TEXT, IN `iPasal_Pelanggaran` TEXT, IN `iMengingat` TEXT)   UPDATE tbl_dpk SET KeputusanSidang = iKeputusanSidang, Update_User = iCreate_User, Update_Date = CURRENT_TIMESTAMP(), Tanggal_Sidang = iTanggal_Sidang, Catatan_Sidang = iCatatan_Sidang, Dasar_Bukti = iDasar_Bukti, Pelanggaran = iPelanggaran, Pasal_Pelanggaran = iPasal_Pelanggaran, Mengingat = iMengingat
WHERE
ID = iid AND NIP = iNIP$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_DPK_Upd_Status` (IN `iid` INT, IN `iNIP` VARCHAR(18), IN `iCreate_User` VARCHAR(100))   UPDATE tbl_dpk 
set Status = 1, update_user = iCreate_User, update_date = CURRENT_TIMESTAMP
WHERE ID = iid and NIP = iNIP$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_DPK_Upd_Status2` (IN `iid` INT, IN `iCreate_User` VARCHAR(100))   UPDATE kasus set Status = 7, Update_Date = Current_Date(), Update_User = iCreate_User
where ID = iid$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_HasilSidang_Create` (IN `iid` INT, IN `iNIP` VARCHAR(18))   SELECT a.id
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
	,CASE 
		WHEN a.Disposisi1_Status = '01'
			THEN 'TL'
            WHEN a.Disposisi1_Status = '02'
			THEN 'Lainnya'
		ELSE ''
		END Disposisi1_Status
	,a.Disposisi1_Notes
	,Disposisi2_By
	,DATE_FORMAT(a.Disposisi2_Date, '%d-%m-%Y') AS Disposisi2_Date
	,CASE 
		WHEN a.Disposisi2_Status = '01'
			THEN 'TL'
            WHEN a.Disposisi2_Status = '02'
			THEN 'Lainnya'
		ELSE ''
		END AS Disposisi2_Status
	,a.Disposisi2_Notes
	,Disposisi3_By
	,DATE_FORMAT(a.Disposisi3_Date, '%d-%m-%Y') AS Disposisi3_Date
	,CASE 
		WHEN a.Disposisi3_Status = '01'
			THEN 'TL'
            WHEN a.Disposisi3_Status = '02'
			THEN 'Lainnya'
		ELSE ''
		END AS Disposisi3_Status
	,a.Disposisi3_Notes
    , f.NIP
    ,f.KeputusanSidang
    ,DATE_FORMAT(f.Tanggal_Sidang, '%d-%m-%Y') as tanggal_sidang_dpk
    ,g.hukuman
    ,f.Catatan_Sidang
,IFNULL(F.Dasar_Bukti,d.Dasar_dan_Bukti_Penunjang) AS Dasar_dan_Bukti_Penunjang
,IFNULL(F.Pelanggaran,d.Pelanggaran) AS Pelanggaran
,IFNULL(F.Pasal_Pelanggaran,d.Pasal_Pelanggaran) AS Pasal_Pelanggaran
,d.Rekomendasi_Hukuman
,d.Analisa_dan_Pertimbangan
,DATE_FORMAT(d.Tanggal_Telaah, '%d-%m-%Y') AS Tanggal_Telaah
,d.TelaahNo
,f.Mengingat
FROM kasus a
LEFT JOIN tbl_satker b ON a.SatuanKerja = b.KODE_SATUAN_KERJA
LEFT JOIN tbl_user c ON c.UserID = a.konseptor
left join tbl_telaah d on a.id = d.ID
LEFT join tbl_pradpk e on a.id = e.ID and d.NIP = e.NIP
left join tbl_dpk f on a.id = f.ID and d.NIP = f.NIP
left join hukdis g on f.KeputusanSidang = g.id
WHERE a.id = iid and d.NIP = iNIP$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_HasilSidang_Sel` (IN `UserID` VARCHAR(25))   SELECT 
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
A.perihal,
c.Catatan,
d.KeputusanSidang as KeputusanSidang,
e.hukuman,
d.Catatan_Sidang,
DATE_FORMAT(c.tanggal_sidang, '%d-%m-%Y') AS tanggal_sidang_pra_dpk,
DATE_FORMAT(d.tanggal_sidang, '%d-%m-%Y') AS tanggal_sidang_dpk
FROM kasus a 
LEFT JOIN tbl_telaah b	on a.id = b.ID
LEFT JOIN tbl_pradpk c on a.id = c.ID and b.NIP = c.NIP
LEFT JOIN tbl_dpk d on a.id = d.ID and b.NIP = d.NIP
left join hukdis e on e.id = d.KeputusanSidang
WHERE IFNULL(A.Status,0) = 7 
and IFNULL(C.Status,0) = 1 
and IFNULL(d.Status,0) = 1
and a.konseptor = UserID$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_HasilSidang_Sel_ByID` (IN `IID` INT, IN `INIP` VARCHAR(18))   SELECT 
A.id,
B.NIP,
B.Fullname,
IFNULL(D.Dasar_Bukti, B.Dasar_dan_Bukti_Penunjang) AS Dasar_dan_Bukti_Penunjang,
IFNULL(D.Pelanggaran, B.Pelanggaran)AS Pelanggaran,
IFNULL(D.Pasal_Pelanggaran, B.Pasal_Pelanggaran)AS Pasal_Pelanggaran,
B.Rekomendasi_Hukuman,
A.nomor_surat,
A.asal_surat,
a.SatuanKerja,
DATE_FORMAT(a.tanggal_surat, '%d-%m-%Y') AS tanggal_surat,
A.perihal,
c.Catatan,
d.KeputusanSidang as KeputusanSidang,
e.hukuman,
d.Catatan_Sidang,
DATE_FORMAT(c.tanggal_sidang, '%d-%m-%Y') AS tanggal_sidang_pra_dpk,
DATE_FORMAT(d.tanggal_sidang, '%d-%m-%Y') AS tanggal_sidang_dpk,
f.Menag,f.Sekjend,g.FullName karopeg, g.NIP nip_karopeg, h.FullName koor, h.NIP nip_koor, i.FullName subkoor, i.NIP nip_subkoor, j.FullName konseptor, j.NIP nip_konseptor,d.Mengingat
FROM kasus a 
LEFT JOIN tbl_telaah b	on a.id = b.ID
LEFT JOIN tbl_pradpk c on a.id = c.ID and b.NIP = c.NIP
LEFT JOIN tbl_dpk d on a.id = d.ID and b.NIP = d.NIP
left join hukdis e on e.id = d.KeputusanSidang
left join tbl_pejabatmst f on 1 = 1
left join tbl_user g on g.UserID = f.Karopeg
left join tbl_user h on h.UserID = f.Koordinator
left join tbl_user i on i.UserID = f.SubKoordinator
left join tbl_user j on j.UserID = a.konseptor
WHERE IFNULL(A.Status,0) = 7
AND A.id = IID AND B.NIP = INIP$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_Hukdis_AlreadyExist` (IN `ihukuman` TEXT)   SELECT * FROM hukdis
WHERE hukuman = ihukuman$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_Hukdis_Del` (IN `iid` INT)   DELETE FROM HUKDIS 
WHERE id = iid$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_Hukdis_Ins` (IN `iHukdisID` VARCHAR(25), IN `iHukuman` TEXT, IN `iTingkat` VARCHAR(25), IN `iLastUser` VARCHAR(100))   INSERT INTO hukdis ( hukuman,tingkat, LastUser, LastUpdate)
VALUES 
(iHukuman, iTingkat, iLastUser, CURRENT_TIMESTAMP)$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_Hukdis_Sel` ()   SELECT 
id,
hukuman,
tingkat,
LastUser,
DATE_FORMAT(LastUpdate,'%d-%m-%Y') as LastUpdate
FROM hukdis$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_Hukdis_Upd` (IN `iid` INT, IN `iHukuman` TEXT, IN `iTingkat` VARCHAR(25), IN `iLastUser` VARCHAR(100))   UPDATE hukdis 
SET 
hukuman = iHukuman,
tingkat = iTingkat,
LastUser = iLastUser,
LastUpdate = CURRENT_TIMESTAMP
WHERE id = iid$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_Karopeg_Sel` ()   SELECT Fullname, UserID FROM tbl_user
WHERE GroupID = '04'
ORDER by fullname$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_Kasus_Monitoring_Sel` (IN `iGroupID` VARCHAR(2), IN `UserLogin` VARCHAR(100))   SELECT  
a.id,
nomor_agenda,
nomor_surat,
asal_surat,
DATE_FORMAT(tanggal_surat, '%d-%m-%Y') AS tanggal_surat,
perihal,
LampiranSurat1,
LampiranSurat2,
LampiranSurat3,
LampiranSurat4,
LampiranSurat5,
konseptor,
SatuanKerja,
IFNULL(a.Status,0) as Status,
DATE_FORMAT(Disposisi1_Date, '%d-%m-%Y') AS Disposisi1_Date,
Disposisi1_By,
Disposisi1_Status,
Disposisi1_Notes,
Disposisi2_By,
DATE_FORMAT(Disposisi2_Date, '%d-%m-%Y') AS Disposisi2_Date,
Disposisi2_Status,
Disposisi2_Notes,
Disposisi3_By,
DATE_FORMAT(Disposisi3_Date, '%d-%m-%Y') AS Disposisi3_Date,
Disposisi3_Status,
Disposisi3_Notes,
DATE_FORMAT(b.tanggal_telaah, '%d-%m-%Y') AS telaah_date,
DATE_FORMAT(c.tanggal_sidang, '%d-%m-%Y') AS pradpk_date,
DATE_FORMAT(d.tanggal_sidang, '%d-%m-%Y') AS dpk_date
FROM kasus a
left join tbl_telaah b on a.id = b.id
left join tbl_pradpk c on a.id = c.id and c.NIP = b.NIP
left join tbl_dpk d on a.id = d.id and b.nip = d.nip
WHERE a.create_user = (CASE WHEN iGroupID = '03' THEN UserLogin ELSE a.create_user  END)
order by tanggal_surat desc$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_Konseptor_Sel` ()   SELECT Fullname, UserID FROM tbl_user
WHERE GroupID = '02'
ORDER by fullname$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_Koordinator_Sel` ()   SELECT Fullname, UserID FROM tbl_user
WHERE GroupID = '05'
ORDER by fullname$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_PasalPelanggaran_Sel` ()   SELECT Pasal_Pelanggaran 
FROM tbl_pasalpelanggaran where id_Hukdis = 1$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_PejabatMst_Ins` (IN `iMenag` VARCHAR(100), IN `iSekjend` VARCHAR(100), IN `iKaropeg` INT, IN `iKoordinator` INT, IN `iSubKoordinator` INT, IN `iCreated_User` VARCHAR(100))   INSERT INTO tbl_pejabatmst (ID, Menag, Sekjend, Karopeg, Koordinator, SubKoordinator,Created_User, Created_Date)
VALUES
(1, iMenag, iSekjend, iKaropeg, iKoordinator, iSubKoordinator,iCreated_User,CURRENT_TIMESTAMP )$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_PejabatMst_Sel` ()   SELECT a.Menag, a.Sekjend, a.Karopeg, a.Koordinator, a.SubKoordinator
FROM 
tbl_pejabatmst a 
left join tbl_user b on a.Karopeg = b.UserID
left join tbl_user c on a.Koordinator = c.UserID
left join tbl_user d on a.SubKoordinator = d.UserID
where a.id = 1$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_PejabatMst_Upd` (IN `iMenag` VARCHAR(100), IN `iSekjend` VARCHAR(100), IN `iKaropeg` INT, IN `iKoordinator` INT, IN `iSubKoordinator` INT, IN `iCreated_User` VARCHAR(100))   UPDATE tbl_pejabatmst 
SET Menag = iMenag,
Sekjend = iSekjend, 
Karopeg = iKaropeg, 
Koordinator = iKoordinator, 
SubKoordinator = iSubKoordinator,
Updated_User = iCreated_User,
Updated_Date = CURRENT_TIMESTAMP
WHERE ID = 1$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_PraDPK_Already_Sel` (IN `iid` INT, IN `iNIP` VARCHAR(18))   SELECT * FROM tbl_pradpk WHERE ID = iid
and NIP = iNIP$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_PraDPK_Ins` (IN `iid` INT, IN `iNIP` VARCHAR(18), IN `iCatatan` TEXT, IN `iCreate_User` VARCHAR(100), IN `iTanggal_Sidang` DATE)   INSERT INTO tbl_pradpk (ID, NIP, Catatan, Create_User, Create_Date, Tanggal_Sidang)
VALUES
(iid, iNIP, iCatatan, iCreate_User, CURRENT_TIMESTAMP(),iTanggal_Sidang)$$

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
A.perihal,
c.Catatan,
DATE_FORMAT(c.Tanggal_Sidang, '%d-%m-%Y') AS Tanggal_Sidang
FROM kasus a 
LEFT JOIN tbl_telaah b	on a.id = b.ID
LEFT JOIN tbl_pradpk c on a.id = c.ID and b.NIP = c.NIP
WHERE IFNULL(A.Status,0) = 5 and IFNULL(C.Status,0) = 0$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_PraDPK_Upd` (IN `iid` INT, IN `iNIP` VARCHAR(18), IN `iCatatan` TEXT, IN `iCreate_User` VARCHAR(100), IN `iTanggal_Sidang` DATE)   UPDATE tbl_pradpk SET Catatan = iCatatan, Update_User = iCreate_User, Update_Date = CURRENT_TIMESTAMP(), Tanggal_Sidang = iTanggal_Sidang
WHERE
ID = iid AND NIP = iNIP$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_PraDPK_Upd_Status` (IN `iid` INT, IN `iNIP` VARCHAR(18), IN `iCreate_User` VARCHAR(100))   UPDATE tbl_pradpk 
set Status = 1, update_user = iCreate_User, update_date = CURRENT_TIMESTAMP
WHERE ID = iid and NIP = iNIP$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_PraDPK_Upd_Status2` (IN `iid` INT, IN `iCreate_User` VARCHAR(100))   UPDATE kasus set Status = 6, Update_Date = Current_Date(), Update_User = iCreate_User
where ID = iid$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_Satker_AlreadyExists` (IN `iSATUAN_KERJA` VARCHAR(255))   SELECT * FROM tbl_satker
WHERE SATUAN_KERJA = iSATUAN_KERJA$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_Satker_Del` (IN `iKODE_SATUAN_KERJA` INT)   DELETE FROM tbl_satker WHERE KODE_SATUAN_KERJA = iKODE_SATUAN_KERJA$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_Satker_GetName` (IN `IID` INT)   SELECT SATUAN_KERJA FROM tbl_satker
WHERE KODE_SATUAN_KERJA = IID$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_Satker_Ins` (IN `iSATUAN_KERJA` VARCHAR(255), IN `iUserID` VARCHAR(100))   insert into tbl_satker(SATUAN_KERJA, LastUser, LastUpdate)
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

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_Satker_Upd` (IN `iKODE_SATUAN_KERJA` INT, IN `iSATUAN_KERJA` VARCHAR(255), IN `iUserID` VARCHAR(100))   UPDATE tbl_satker SET SATUAN_KERJA = iSATUAN_KERJA,
LastUser = iUserID, LastUpdate = CURRENT_TIME
WHERE KODE_SATUAN_KERJA = iKODE_SATUAN_KERJA$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_SubKoordinator_Sel` ()   SELECT Fullname, UserID FROM tbl_user
WHERE GroupID = '06'
ORDER by fullname$$

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

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_SuratMasuk_Filter_Sel` (IN `iStatus` INT, IN `DateFrom` DATE, IN `DateTo` DATE, IN `iGroupID` VARCHAR(100), IN `UserLogin` VARCHAR(100))   IF iStatus = 0 THEN
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
        ,CASE 
            WHEN a.Disposisi1_Status = '01'
                THEN 'TL'
            WHEN a.Disposisi1_Status = '02' THEN 'Lainnya' 
ELSE ''
            END Disposisi1_Status
        ,a.Disposisi1_Notes
        ,Disposisi2_By
        ,DATE_FORMAT(a.Disposisi2_Date, '%d-%m-%Y') AS Disposisi2_Date
        ,CASE 
            WHEN a.Disposisi2_Status= '01'
                THEN 'TL'
            WHEN a.Disposisi2_Status= '02' THEN 'Lainnya' 
ELSE ''
            END AS Disposisi2_Status
        ,a.Disposisi2_Notes
        ,Disposisi3_By
        ,DATE_FORMAT(a.Disposisi3_Date, '%d-%m-%Y') AS Disposisi3_Date
        ,CASE 
            WHEN a.Disposisi3_Status= '01'
                THEN 'TL'
            WHEN a.Disposisi3_Status= '02' THEN 'Lainnya' 
ELSE ''
            END AS Disposisi3_Status
        ,a.Disposisi3_Notes
    FROM kasus a
    LEFT JOIN tbl_satker b ON a.SatuanKerja = b.KODE_SATUAN_KERJA
    LEFT JOIN tbl_user c ON c.UserID = a.konseptor
    WHERE IFNULL(a.STATUS, 0) = 0 
    and a.create_user = (CASE WHEN iGroupID = '03' THEN UserLogin ELSE a.create_user END)
    and a.tanggal_surat between DateFrom and DateTo;
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
        ,CASE 
		WHEN a.Disposisi1_Status = '01'
			THEN 'TL'
            WHEN a.Disposisi1_Status = '02'
			THEN 'Lainnya'
		ELSE ''
		END Disposisi1_Status
        ,a.Disposisi1_Notes
        ,Disposisi2_By
        ,DATE_FORMAT(a.Disposisi2_Date, '%d-%m-%Y') AS Disposisi2_Date
        ,CASE 
		WHEN a.Disposisi2_Status = '01'
			THEN 'TL'
            WHEN a.Disposisi2_Status = '02'
			THEN 'Lainnya'
		ELSE ''
		END AS Disposisi2_Status
        ,a.Disposisi2_Notes
        ,Disposisi3_By
        ,DATE_FORMAT(a.Disposisi3_Date, '%d-%m-%Y') AS Disposisi3_Date
        ,CASE 
		WHEN a.Disposisi3_Status = '01'
			THEN 'TL'
            WHEN a.Disposisi3_Status = '02'
			THEN 'Lainnya'
		ELSE ''
		END AS Disposisi3_Status
        ,a.Disposisi3_Notes
    FROM kasus a
    LEFT JOIN tbl_satker b ON a.SatuanKerja = b.KODE_SATUAN_KERJA
    LEFT JOIN tbl_user c ON c.UserID = a.konseptor
    WHERE IFNULL(a.STATUS, 0) != 0 
    and a.tanggal_surat between DateFrom and DateTo
    and a.create_user = (CASE WHEN iGroupID = '03' THEN UserLogin ELSE a.create_user END);
END IF$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_SuratMasuk_GetKonseptor` (IN `iKODE_SATUAN_KERJA` INT)   SELECT konseptor FROM tbl_konseptor_satker
WHERE KODE_SATUAN_KERJA = iKODE_SATUAN_KERJA$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_SuratMasuk_Ins` (IN `inomor_agenda` VARCHAR(25), IN `inomor_surat` VARCHAR(100), IN `iasal_surat` VARCHAR(255), IN `itanggal_surat` DATETIME, IN `iperihal` VARCHAR(255), IN `iLampiranSurat1` VARCHAR(255), IN `iLampiranSurat2` VARCHAR(255), IN `iLampiranSurat3` VARCHAR(255), IN `iLampiranSurat4` VARCHAR(255), IN `iLampiranSurat5` VARCHAR(255), IN `iLampiranSurat6` VARCHAR(255), IN `iSatuanKerja` VARCHAR(255), IN `icreate_user` VARCHAR(100), IN `iLampiran_LHA` VARCHAR(255))   INSERT INTO kasus
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

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_SuratMasuk_Sel` (IN `iid` INT)   SELECT a.id
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
	,CASE 
		WHEN a.Disposisi1_Status = '01'
			THEN 'TL'
            WHEN a.Disposisi1_Status = '02'
			THEN 'Lainnya'
		ELSE ''
		END Disposisi1_Status
	,a.Disposisi1_Notes
	,Disposisi2_By
	,DATE_FORMAT(a.Disposisi2_Date, '%d-%m-%Y') AS Disposisi2_Date
	,CASE 
		WHEN a.Disposisi2_Status = '01'
			THEN 'TL'
            WHEN a.Disposisi2_Status = '02'
			THEN 'Lainnya'
		ELSE ''
		END AS Disposisi2_Status
	,a.Disposisi2_Notes
	,Disposisi3_By
	,DATE_FORMAT(a.Disposisi3_Date, '%d-%m-%Y') AS Disposisi3_Date
	,CASE 
		WHEN a.Disposisi3_Status = '01'
			THEN 'TL'
            WHEN a.Disposisi3_Status = '02'
			THEN 'Lainnya'
		ELSE ''
		END AS Disposisi3_Status
	,a.Disposisi3_Notes
FROM kasus a
LEFT JOIN tbl_satker b ON a.SatuanKerja = b.KODE_SATUAN_KERJA
LEFT JOIN tbl_user c ON c.UserID = a.konseptor
WHERE a.id = iid$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_SuratMasuk_Upd` (IN `iid` INT, IN `inomor_agenda` VARCHAR(25), IN `inomor_surat` VARCHAR(100), IN `iasal_surat` VARCHAR(255), IN `itanggal_surat` DATETIME, IN `iperihal` VARCHAR(255), IN `iLampiranSurat1` VARCHAR(255), IN `iLampiranSurat2` VARCHAR(255), IN `iLampiranSurat3` VARCHAR(255), IN `iLampiranSurat4` VARCHAR(255), IN `iLampiranSurat5` VARCHAR(255), IN `iLampiranSurat6` VARCHAR(255), IN `iSatuanKerja` VARCHAR(255), IN `iupdate_user` VARCHAR(100), IN `iLampiran_LHA` VARCHAR(255))   UPDATE kasus
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

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_Telaah_Ins` (IN `iID` INT, IN `iNIP` VARCHAR(25), IN `iDasar_dan_Bukti_Penunjang` TEXT, IN `iPelanggaran` TEXT, IN `iPasal_Pelanggaran` TEXT, IN `iRekomendasi_Hukuman` TEXT, IN `iAnalisa_dan_Pertimbangan` TEXT, IN `iKeputusan_Sidang_DPK` TEXT, IN `iCreated_User` VARCHAR(100), IN `iFullname` VARCHAR(255), IN `iTelaahNo` VARCHAR(100), IN `iTanggalTelaah` DATE)   INSERT INTO tbl_telaah
(ID,NIP,Dasar_dan_Bukti_Penunjang,Pelanggaran,
 Pasal_Pelanggaran,Rekomendasi_Hukuman,
 Analisa_dan_Pertimbangan,Keputusan_Sidang_DPK,
 Created_User,Created_Date,Fullname, TelaahNo, Tanggal_Telaah)
 VALUES
 (iID,iNIP,iDasar_dan_Bukti_Penunjang,iPelanggaran,
iPasal_Pelanggaran,iRekomendasi_Hukuman,iAnalisa_dan_Pertimbangan,
iKeputusan_Sidang_DPK,iCreated_User,CURRENT_TIMESTAMP,iFullname,iTelaahNo,iTanggalTelaah)$$

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
a.TelaahNo,
DATE_FORMAT(a.Tanggal_Telaah,'%d-%m-%Y') as Tanggal_Telaah,
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

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_Telaah_Upd` (IN `iID` INT, IN `iNIP` VARCHAR(25), IN `iDasar_dan_Bukti_Penunjang` TEXT, IN `iPelanggaran` TEXT, IN `iPasal_Pelanggaran` TEXT, IN `iRekomendasi_Hukuman` TEXT, IN `iAnalisa_dan_Pertimbangan` TEXT, IN `iKeputusan_Sidang_DPK` TEXT, IN `iCreated_User` VARCHAR(100), IN `iFileTelaah` TEXT, IN `iFullname` VARCHAR(255), IN `iTelaahNo` VARCHAR(100), IN `iTanggalTelaah` DATE)   UPDATE tbl_telaah
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
TelaahNo = iTelaahNo,
tanggal_telaah = iTanggalTelaah
WHERE
ID = iID
AND NIP = iNIP$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_Telaah_UpdStatus` (IN `iID` INT, IN `iNIP` VARCHAR(18), IN `iCreated_User` VARCHAR(100))   UPDATE KASUS SET STATUS = 5, update_user = iCreated_User,
update_date = CURRENT_TIMESTAMP
WHERE ID = iID$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_Telaah_UpdTelaahStatus` (IN `iID` INT, IN `iNIP` VARCHAR(18), IN `iCreated_User` VARCHAR(100), IN `iFileTelaah` TEXT)   UPDATE tbl_telaah set Proses = 1, Updated_User = iCreated_User, Created_Date = CURRENT_TIMESTAMP, FileTelaah = iFileTelaah
where ID = iID AND NIP = iNIP$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_Tembusan_sel` (IN `iKODE_SATUAN_KERJA` INT, IN `iSATUAN_KERJA` VARCHAR(255))   select Tembusan from
(SELECT tembusan AS Tembusan, Tipe FROM tbl_tembusan where KODE_SATUAN_KERJA = iKODE_SATUAN_KERJA and Tipe <> 9
UNION ALL
SELECT taspen AS Tembusan, '10' Tipe  from tbl_taspen  where KODE_SATUAN_KERJA = iKODE_SATUAN_KERJA and Tembusan =iSATUAN_KERJA
UNION ALL
SELECT kppn AS Tembusan, '11' Tipe  from tbl_kppn  where KODE_SATUAN_KERJA = iKODE_SATUAN_KERJA and Tembusan =iSATUAN_KERJA
UNION ALL
SELECT tembusan AS Tembusan, Tipe FROM tbl_tembusan where KODE_SATUAN_KERJA = iKODE_SATUAN_KERJA and Tipe = 9 AND Tembusan =iSATUAN_KERJA)a order by Tipe$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_UserGroup_AlreadyExist` (IN `iGroupID` VARCHAR(25))   SELECT * FROM tbl_usergroup WHERE GroupID = iGroupID$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_UserGroup_Del` (IN `iGroupID` VARCHAR(25))   DELETE FROM tbl_usergroup
WHERE GroupID = iGroupID$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_UserGroup_Ins` (IN `iGroupID` VARCHAR(25), IN `iGroupDesc` VARCHAR(25), IN `iUserLogin` VARCHAR(100))   INSERT INTO tbl_usergroup (GroupID, GroupDesc, LastUser, LastUpdate)
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
a.Satker,
c.SATUAN_KERJA
FROM tbl_user a
LEFT JOIN tbl_usergroup b ON a.GroupID = b.GroupID
LEFT join tbl_satker c on a.Satker = c.KODE_SATUAN_KERJA$$

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
  `LastUser` varchar(100) NOT NULL,
  `LastUpdate` datetime DEFAULT NULL
) ENGINE=MyISAM DEFAULT CHARSET=latin1 ROW_FORMAT=DYNAMIC;

--
-- Dumping data for table `hukdis`
--

INSERT INTO `hukdis` (`id`, `hukuman`, `tingkat`, `LastUser`, `LastUpdate`) VALUES
(1, 'Teguran Lisan', 'Ringan', 'administrator', '2022-05-27 20:42:04'),
(2, 'Teguran Tertulis', 'Ringan', 'administrator', '2022-05-27 20:43:03'),
(3, 'Pernyataan Tidak Puas Secara Tertulis', 'Ringan', 'administrator', '2022-05-27 20:43:28'),
(4, 'Pemotongan Tunjangan Kinerja Sebesar 25% Selama 6 Bulan', 'Sedang', 'administrator', '2022-05-27 20:47:18'),
(5, 'Pemotongan Tunjangan Kinerja Sebesar 25% Selama 9 Bulan', 'Sedang', 'administrator', '2022-05-27 20:47:33'),
(6, 'Pemotongan Tunjangan Kinerja Sebesar 25% Selama 12 Bulan', 'Sedang', 'administrator', '2022-05-27 20:47:46'),
(7, 'Penurunan Jabatan Setingkat Lebih Rendah Selama 12 Bulan', 'Berat', 'administrator', '2022-05-27 20:48:10'),
(8, 'Pembebasan Dari Jabatannya Menjadi Jabatan Pelaksana Selama 12 Bulan', 'Berat', 'administrator', '2022-05-27 20:48:30'),
(9, 'Pemberhentian Dengan Hormat Tidak Atas Permintaan Sendiri Sebagai PNS', 'Berat', 'administrator', '2022-05-27 20:48:55'),
(10, 'Penundaan Kenaikan Gaji Berkala selama 1 (satu) tahun', 'Sedang', 'administrator', '2022-06-01 10:27:59'),
(11, 'Penundaan Kenaikan Pangkat selama 1 (satu) Tahun', 'Sedang', 'administrator', '2022-06-01 10:28:28'),
(12, 'Penurunan Pangkat Setingkat Lebih Rendah Selama 1 (satu) Tahun', 'Sedang', 'administrator', '2022-06-01 10:29:35'),
(13, 'Penurunan Pangkat Setingkat Lebih Rendah Selama 3 (tiga) Tahun', 'Berat', 'administrator', '2022-06-01 10:30:17'),
(14, 'Pemindahan dalam rangka penurunan jabatan setingkat lebih rendah', 'Berat', 'administrator', '2022-06-01 10:30:52'),
(15, 'Pemberhentian tidak dengan hormat sebagai PNS', 'Berat', 'administrator', '2022-06-01 10:31:25');

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
  `create_user` text NOT NULL,
  `update_date` datetime DEFAULT NULL,
  `update_user` varchar(100) NOT NULL,
  `Status` int(1) NOT NULL DEFAULT 1,
  `Disposisi1_Date` datetime DEFAULT NULL,
  `Disposisi1_By` varchar(100) NOT NULL,
  `Disposisi1_Status` varchar(25) NOT NULL,
  `Disposisi1_Notes` text NOT NULL,
  `Disposisi2_By` varchar(100) NOT NULL,
  `Disposisi2_Date` datetime DEFAULT NULL,
  `Disposisi2_Status` varchar(25) NOT NULL,
  `Disposisi2_Notes` text NOT NULL,
  `Disposisi3_By` varchar(100) DEFAULT NULL,
  `Disposisi3_Date` datetime DEFAULT NULL,
  `Disposisi3_Status` varchar(25) NOT NULL,
  `Disposisi3_Notes` text NOT NULL,
  `Dokumen_Yang_Akan_Dibuat` varchar(2) NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=latin1 ROW_FORMAT=DYNAMIC;

--
-- Dumping data for table `kasus`
--

INSERT INTO `kasus` (`id`, `nomor_agenda`, `nomor_surat`, `asal_surat`, `tanggal_surat`, `perihal`, `LampiranSurat1`, `LampiranSurat2`, `LampiranSurat3`, `LampiranSurat4`, `LampiranSurat5`, `LampiranSurat6`, `Lampiran_LHA`, `konseptor`, `SatuanKerja`, `created_date`, `create_user`, `update_date`, `update_user`, `Status`, `Disposisi1_Date`, `Disposisi1_By`, `Disposisi1_Status`, `Disposisi1_Notes`, `Disposisi2_By`, `Disposisi2_Date`, `Disposisi2_Status`, `Disposisi2_Notes`, `Disposisi3_By`, `Disposisi3_Date`, `Disposisi3_Status`, `Disposisi3_Notes`, `Dokumen_Yang_Akan_Dibuat`) VALUES
(24, '', 'R-1911/Kw/09.1/3/Kp04.1/02/2022', 'Kanwil Kementerian Agama Provinsi DKI Jakarta', '2022-02-18', 'Permohonan Penjatuhan Hukuman Disiplin PNS atas nama Drs. Adam', 'PP 53 Tahun 2010_01062022_063105.pdf', '', '', '', '', '', '', '71', '12', '2022-06-01 18:31:05', 'Drs. H. NYAK DIN ACEH, MM', '2022-06-02 00:00:00', 'WIDI TANURJAYA, S.H', 7, '2022-06-01 18:32:52', 'Dr. NURUDIN, S.Pd.I., M.Si.', '01', 'go', 'SEPTIAN SAPUTRA, S.Kom', '2022-06-01 18:33:12', '01', 'goes', 'HJ. AZIEZAH KEBAHYANG, S.H., M.H', '2022-06-01 18:33:58', '01', 'buat telaahan', '01'),
(23, '', 'R-10251/Kw.10/KP.04.2/11/2021', 'Kanwil Kementerian Agama Provinsi Jawa Barat', '2021-11-17', 'Penjatuhan Hukuman Disiplin Pegawai Negeri Sipil a.n. Muhammad Ikbal, S.Ag.Tim Kantor Kementerian Agama Kabupaten Bogor', 'Usul Hukdis Itjen_30052022_125228.pdf', '', '', '', '', '', '', '76', '13', '2022-05-30 12:52:28', 'PIPIT WAHYUDIN , S.Pd.I', '2022-05-30 13:25:16', 'ALAMSYAH, S.E', 4, '2022-05-30 12:53:14', 'Dr. NURUDIN, S.Pd.I., M.Si.', '01', 'TINDAK LANJUTI', 'SEPTIAN SAPUTRA, S.Kom', '2022-05-30 12:53:37', '01', 'TINDAK LANJUTI', 'HJ. AZIEZAH KEBAHYANG, S.H., M.H', '2022-05-30 12:54:05', '01', 'TINDAK LANJUTI', '01'),
(21, NULL, 'R-439/IJ/PS.01.3/05/2019', 'Inspektorat Jenderal', '2019-05-22', 'Laporan Hasil Audit Tujuan Tertentu Terkait Pelanggaran Disiplin Pegawai pada Kantor Kementerian Agama Kabupaten Garut Provinsi Jawa Barat', 'Usul Hukdis Itjen_26052022_102635.pdf', '', '', '', '', '', '23052022_SURTUG PRA SIDANG TGL 19 Mei 2022_ds_26052022_102635.pdf', '76', '13', '2022-05-26 22:26:35', 'H. NUGRAHA STIAWAN, S.Sos.I., M.Ak.', '2022-05-29 00:00:00', 'ALAMSYAH, S.E', 7, '2022-05-27 00:19:32', 'Dr. NURUDIN, S.Pd.I., M.Si.', '01', 'proses', 'SEPTIAN SAPUTRA, S.Kom', '2022-05-27 01:27:26', '01', 'telaah', 'HJ. AZIEZAH KEBAHYANG, S.H., M.H', '2022-05-27 01:28:12', '01', 'telaah', '01'),
(22, '', 'R-691/KW.30/1-c/KP.04.2/02/2022', 'Kanwil Kementerian Agama Provinsi Gorontalo', '2022-04-03', 'Usul Penjatuhan Hukuman Disiplin an. Jafar Harun, S.Pd.I.', '28032022_SURAT TUGAS USUL SATYA HUT RI 2022_ds_30052022_124927.pdf', '', '', '', '', '', '', '68', '23', '2022-05-30 12:49:27', 'DJAMILA BALADRAF, S.Ag, M.H.I', '2022-05-30 12:54:14', 'HJ. AZIEZAH KEBAHYANG, S.H., M.H', 4, '2022-05-30 12:53:18', 'Dr. NURUDIN, S.Pd.I., M.Si.', '01', 'TINDAK LANJUTI', 'SEPTIAN SAPUTRA, S.Kom', '2022-05-30 12:53:40', '01', 'TINDAK LANJUTI', 'HJ. AZIEZAH KEBAHYANG, S.H., M.H', '2022-05-30 12:54:14', '01', 'TINDAK LANJUTI', '01');

-- --------------------------------------------------------

--
-- Table structure for table `tbl_dpk`
--

CREATE TABLE `tbl_dpk` (
  `ID` int(11) NOT NULL,
  `NIP` varchar(18) NOT NULL,
  `Tanggal_Sidang` date NOT NULL,
  `KeputusanSidang` int(11) NOT NULL,
  `Catatan_Sidang` text NOT NULL,
  `Dasar_Bukti` text NOT NULL,
  `Pelanggaran` text NOT NULL,
  `Pasal_Pelanggaran` text NOT NULL,
  `Mengingat` text NOT NULL,
  `Status` int(11) NOT NULL,
  `Create_User` varchar(100) NOT NULL,
  `Create_Date` datetime NOT NULL,
  `Update_User` varchar(100) NOT NULL,
  `Update_Date` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `tbl_dpk`
--

INSERT INTO `tbl_dpk` (`ID`, `NIP`, `Tanggal_Sidang`, `KeputusanSidang`, `Catatan_Sidang`, `Dasar_Bukti`, `Pelanggaran`, `Pasal_Pelanggaran`, `Mengingat`, `Status`, `Create_User`, `Create_Date`, `Update_User`, `Update_Date`) VALUES
(21, '197002052000031001', '2022-06-03', 28, 'test', '', '', '', '', 1, 'ALAMSYAH, S.E', '2022-05-27 20:15:05', 'ALAMSYAH, S.E', '2022-05-29 19:08:43'),
(24, '196906172003121001', '2022-05-31', 1, '', 'Surat Kepala Kantor Kementerian Agama Provinsi DKI Jakarta Agama Nomor R-1911/Kw/09.1/3/Kp04.1/02/2022 tanggal 18 Februari 2022 perihal Permohonan Penjatuhan Hukuman Disiplin PNS atas nama Drs. Adam, dan Berita Acara Pemeriksaan (BAP) tanggal 29 Desember 2021 terhadap Sdr. Drs. Adam NIP. 196906172003121001', 'tindak pidana kekerasan dalam rumah tangga terhadap anak kandung dan istri sahnya', 'Pasal 3 huruf f Pemerintah Nomor 94 Tahun 2021 tentang Disiplin Pegawai Negeri Sipil', 'Undang-Undang Nomor 5 Tahun 2014 tentang Aparatur Sipil Negara;\nPeraturan Pemerintah Nomor 53 Tahun 2010 tentang Disiplin Pegawai Negeri Sipil;\nPeraturan Pemerintah Nomor 9 Tahun 2003 jo. Nomor 63 Tahun 2009;\nPeraturan Pemerintah Nomor 7 Tahun 1977 tentang Gaji Pegawai Negeri Sipil sebagaimana telah beberapa kali diubah terakhir dengan Peraturan Pemerintah Nomor 15 Tahun 2019; \nPeraturan Kepala Badan Kepegawaian Negara Nomor 21 Tahun 2010;\nPeraturan Menteri Agama Nomor 2 Tahun 2014 tentang Dewan Petimbangan Kepegawaian Kementerian Agama;', 1, 'WIDI TANURJAYA, S.H', '2022-06-02 08:04:44', 'WIDI TANURJAYA, S.H', '2022-06-06 08:37:18');

-- --------------------------------------------------------

--
-- Table structure for table `tbl_konseptor_satker`
--

CREATE TABLE `tbl_konseptor_satker` (
  `KODE_SATUAN_KERJA` int(11) NOT NULL,
  `Konseptor` int(11) NOT NULL,
  `LastUser` varchar(100) NOT NULL,
  `LastUpdate` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `tbl_konseptor_satker`
--

INSERT INTO `tbl_konseptor_satker` (`KODE_SATUAN_KERJA`, `Konseptor`, `LastUser`, `LastUpdate`) VALUES
(1, 69, 'administrator', '2022-05-22 13:18:33'),
(2, 69, 'administrator', '2022-05-22 13:18:49'),
(3, 69, 'administrator', '2022-05-22 13:18:59'),
(4, 70, 'administrator', '2022-05-22 13:19:06'),
(5, 70, 'administrator', '2022-05-22 13:19:13'),
(6, 68, 'administrator', '2022-05-22 13:19:21'),
(7, 69, 'administrator', '2022-05-22 13:19:28'),
(8, 68, 'administrator', '2022-05-22 13:19:38'),
(9, 69, 'administrator', '2022-05-22 13:19:50'),
(10, 68, 'administrator', '2022-05-22 13:19:58'),
(11, 75, 'administrator', '2022-05-22 13:20:04'),
(12, 71, 'administrator', '2022-05-22 13:20:22'),
(13, 76, 'administrator', '2022-05-22 13:20:29'),
(23, 68, 'administrator', '2022-05-22 13:20:40');

-- --------------------------------------------------------

--
-- Table structure for table `tbl_kppn`
--

CREATE TABLE `tbl_kppn` (
  `KODE_SATUAN_KERJA` int(11) NOT NULL,
  `Tembusan` varchar(255) NOT NULL,
  `KPPN` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

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
(2, 5, 2022),
(3, 5, 2022),
(1, 6, 2022);

-- --------------------------------------------------------

--
-- Table structure for table `tbl_pasalpelanggaran`
--

CREATE TABLE `tbl_pasalpelanggaran` (
  `id_Hukdis` int(11) NOT NULL,
  `Pasal_Pelanggaran` text NOT NULL,
  `Created_Date` datetime NOT NULL,
  `Created_User` varchar(100) NOT NULL,
  `Updated_Date` datetime NOT NULL,
  `Updated_User` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `tbl_pasalpelanggaran`
--

INSERT INTO `tbl_pasalpelanggaran` (`id_Hukdis`, `Pasal_Pelanggaran`, `Created_Date`, `Created_User`, `Updated_Date`, `Updated_User`) VALUES
(1, 'Undang-Undang Nomor 5 Tahun 2014 tentang Aparatur Sipil Negara;', '0000-00-00 00:00:00', '', '0000-00-00 00:00:00', ''),
(1, 'Peraturan Pemerintah Nomor 53 Tahun 2010 tentang Disiplin Pegawai Negeri Sipil;', '0000-00-00 00:00:00', '', '0000-00-00 00:00:00', ''),
(1, 'Peraturan Pemerintah Nomor 9 Tahun 2003 jo. Nomor 63 Tahun 2009;', '0000-00-00 00:00:00', '', '0000-00-00 00:00:00', ''),
(1, 'Peraturan Pemerintah Nomor 7 Tahun 1977 tentang Gaji Pegawai Negeri Sipil sebagaimana telah beberapa kali diubah terakhir dengan Peraturan Pemerintah Nomor 15 Tahun 2019; ', '0000-00-00 00:00:00', '', '0000-00-00 00:00:00', ''),
(1, 'Peraturan Kepala Badan Kepegawaian Negara Nomor 21 Tahun 2010;', '0000-00-00 00:00:00', '', '0000-00-00 00:00:00', ''),
(1, 'Peraturan Menteri Agama Nomor 2 Tahun 2014 tentang Dewan Petimbangan Kepegawaian Kementerian Agama;', '0000-00-00 00:00:00', '', '0000-00-00 00:00:00', '');

-- --------------------------------------------------------

--
-- Table structure for table `tbl_pejabatmst`
--

CREATE TABLE `tbl_pejabatmst` (
  `Menag` varchar(100) NOT NULL,
  `Sekjend` varchar(100) NOT NULL,
  `Karopeg` int(11) NOT NULL,
  `Koordinator` int(11) NOT NULL,
  `SubKoordinator` int(11) NOT NULL,
  `ID` int(11) NOT NULL,
  `Created_User` varchar(100) NOT NULL,
  `Created_Date` datetime NOT NULL,
  `Updated_User` varchar(100) NOT NULL,
  `Updated_Date` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `tbl_pejabatmst`
--

INSERT INTO `tbl_pejabatmst` (`Menag`, `Sekjend`, `Karopeg`, `Koordinator`, `SubKoordinator`, `ID`, `Created_User`, `Created_Date`, `Updated_User`, `Updated_Date`) VALUES
('Yaqut Cholil Qoumas', 'Nizar', 78, 72, 77, 1, 'administrator', '2022-06-01 16:30:54', 'administrator', '2022-06-01 17:31:37');

-- --------------------------------------------------------

--
-- Table structure for table `tbl_pradpk`
--

CREATE TABLE `tbl_pradpk` (
  `ID` int(11) NOT NULL,
  `NIP` varchar(18) NOT NULL,
  `Tanggal_Sidang` datetime DEFAULT NULL,
  `Catatan` text NOT NULL,
  `Status` int(11) NOT NULL,
  `Create_User` varchar(100) NOT NULL,
  `Create_Date` datetime NOT NULL,
  `Update_User` varchar(100) NOT NULL,
  `Update_Date` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `tbl_pradpk`
--

INSERT INTO `tbl_pradpk` (`ID`, `NIP`, `Tanggal_Sidang`, `Catatan`, `Status`, `Create_User`, `Create_Date`, `Update_User`, `Update_Date`) VALUES
(24, '196906172003121001', '2022-05-19 00:00:00', '', 1, 'WIDI TANURJAYA, S.H', '2022-06-02 08:00:39', 'WIDI TANURJAYA, S.H', '2022-06-02 08:01:22'),
(21, '197002052000031001', '2022-06-03 00:00:00', '26', 1, 'ALAMSYAH, S.E', '2022-05-27 16:57:29', 'ALAMSYAH, S.E', '2022-05-27 22:36:38');

-- --------------------------------------------------------

--
-- Table structure for table `tbl_satker`
--

CREATE TABLE `tbl_satker` (
  `KODE_SATUAN_KERJA` int(11) NOT NULL,
  `SATUAN_KERJA` varchar(255) NOT NULL,
  `LastUser` varchar(100) NOT NULL,
  `LastUpdate` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `tbl_satker`
--

INSERT INTO `tbl_satker` (`KODE_SATUAN_KERJA`, `SATUAN_KERJA`, `LastUser`, `LastUpdate`) VALUES
(1, 'Kanwil Kementerian Agama Provinsi Aceh', 'administrator', '2022-05-18 09:03:14'),
(2, 'Kanwil Kementerian Agama Provinsi Sumatera Utara', 'administrator', '2022-05-18 09:03:23'),
(3, 'Kanwil Kementerian Agama Provinsi Sumatera Barat', 'administrator', '2022-05-18 08:58:12'),
(4, 'Kanwil Kementerian Agama Provinsi Riau', 'administrator', '2022-05-18 09:06:10'),
(5, 'Kanwil Kementerian Agama Provinsi Kepulauan Riau', 'administrator', '2022-05-18 09:05:23'),
(6, 'Kanwil Kementerian Agama Provinsi Jambi', 'administrator', '2022-05-18 09:05:46'),
(7, 'Kanwil Kementerian Agama Provinsi Sumatera Selatan', 'administrator', '2022-05-18 09:06:46'),
(8, 'Kanwil Kementerian Agama Provinsi Bengkulu', 'administrator', '2022-05-18 09:06:55'),
(9, 'Kanwil Kementerian Agama Provinsi Lampung', 'administrator', '2022-05-18 10:29:34'),
(10, 'Kanwil Kementerian Agama Provinsi Kep. Bangka Belitung', 'administrator', '2022-05-18 10:29:42'),
(11, 'Kanwil Kementerian Agama Provinsi Banten', 'administrator', '2022-05-18 10:30:05'),
(12, 'Kanwil Kementerian Agama Provinsi DKI Jakarta', '', NULL),
(13, 'Kanwil Kementerian Agama Provinsi Jawa Barat', '', NULL),
(14, 'Kanwil Kementerian Agama Provinsi Jawa Tengah', '', NULL),
(15, 'Kanwil Kementerian Agama Provinsi DI Yogyakarta', '', NULL),
(16, 'Kanwil Kementerian Agama Provinsi Jawa Timur', 'administrator', '2022-05-18 10:39:50'),
(17, 'Kanwil Kementerian Agama Provinsi Kalimantan Barat', 'administrator', '2022-05-18 10:40:52'),
(18, 'Kanwil Kementerian Agama Provinsi Kalimantan Tengah', 'administrator', '2022-05-18 10:41:03'),
(19, 'Kanwil Kementerian Agama Provinsi Kalimantan Selatan', 'administrator', '2022-05-18 10:41:11'),
(20, 'Kanwil Kementerian Agama Provinsi Kalimantan Timur', 'administrator', '2022-05-18 10:41:31'),
(21, 'Kanwil Kementerian Agama Provinsi Kalimantan Utara', 'administrator', '2022-05-18 10:41:46'),
(22, 'Kanwil Kementerian Agama Provinsi Sulawesi Utara', 'administrator', '2022-05-18 10:42:21'),
(23, 'Kanwil Kementerian Agama Provinsi Gorontalo', 'administrator', '2022-05-18 10:54:58'),
(24, 'Kanwil Kementerian Agama Provinsi Sulawesi Tengah', '', NULL),
(25, 'Kanwil Kementerian Agama Provinsi Sulawesi Tenggara', '', NULL),
(26, 'Kanwil Kementerian Agama Provinsi Sulawesi Barat', 'administrator', '2022-05-18 10:54:36'),
(27, 'Kanwil Kementerian Agama Provinsi Sulawesi Selatan', 'administrator', '2022-05-18 09:02:52'),
(28, 'Kanwil Kementerian Agama Provinsi Bali', '', NULL),
(29, 'Kanwil Kementerian Agama Provinsi Nusa Tenggara Barat', '', NULL),
(30, 'Kanwil Kementerian Agama Provinsi Nusa Tenggara Timur', '', NULL),
(31, 'Kanwil Kementerian Agama Provinsi Maluku', '', NULL),
(32, 'Kanwil Kementerian Agama Provinsi Maluku Utara', '', NULL),
(33, 'Kanwil Kementerian Agama Provinsi Papua', '', NULL),
(34, 'Kanwil Kementerian Agama Provinsi Papua Barat', '', NULL),
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
(51, 'IAIN Syekh Nurjati Cirebon', '', NULL),
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
(135, 'Sekretariat Jenderal', 'administrator', '2022-05-18 09:00:53'),
(136, 'Direktorat Jenderal Pendidikan Islam', '', NULL),
(137, 'Direktorat Jenderal Bimbingan Masyarakat Islam', '', NULL),
(138, 'Direktorat Jenderal Bimbingan Masyarakat Kristen', '', NULL),
(139, 'Direktorat Jenderal Bimbingan Masyarakat Katolik', '', NULL),
(140, 'Direktorat Jenderal Bimbingan Masyarakat Hindu', '', NULL),
(141, 'Direktorat Jenderal Bimbingan Masyarakat Buddha', '', NULL),
(142, 'Direktorat Jenderal Penyelenggaraan Haji dan Umrah', '', NULL),
(143, 'Inspektorat Jenderal', 'administrator', '2022-05-18 09:01:58'),
(144, 'Badan Litbang Serta Pendidikan dan Pelatihan', '', NULL),
(145, 'Badan Penyelenggara Jaminan Produk Halal', '', NULL),
(146, 'UHN Bagus Sugriwa Denpasar', '', NULL);

-- --------------------------------------------------------

--
-- Table structure for table `tbl_taspen`
--

CREATE TABLE `tbl_taspen` (
  `KODE_SATUAN_KERJA` int(11) NOT NULL,
  `Tembusan` varchar(255) NOT NULL,
  `Taspen` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `tbl_taspen`
--

INSERT INTO `tbl_taspen` (`KODE_SATUAN_KERJA`, `Tembusan`, `Taspen`) VALUES
(104, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Jakarta'),
(111, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Ambon'),
(105, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Bandung'),
(108, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Banjarmasin'),
(112, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Denpasar'),
(113, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Jakarta'),
(110, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Makassar'),
(101, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Medan'),
(109, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Manado'),
(102, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Padang'),
(103, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Palembang'),
(106, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Semarang'),
(107, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Surabaya'),
(115, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Makassar'),
(114, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Semarang'),
(141, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Jakarta'),
(140, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Jakarta'),
(137, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Jakarta'),
(139, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Jakarta'),
(138, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Jakarta'),
(136, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Jakarta'),
(142, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Jakarta'),
(57, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Ambon'),
(55, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Banjarmasin'),
(35, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Banda Aceh'),
(45, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Padang'),
(58, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Mataram'),
(48, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Palembang'),
(49, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Lampung'),
(56, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Gorontalo'),
(50, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Serang'),
(47, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Jambi'),
(43, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Medan'),
(40, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Surabaya'),
(51, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Cirebon'),
(52, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Semarang'),
(95, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Denpasar'),
(1, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Banda Aceh'),
(1, 'Kepala Kantor Kementerian Agama Kabupaten Aceh Barat', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Banda Aceh'),
(1, 'Kepala Kantor Kementerian Agama Kabupaten Aceh Besar', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Banda Aceh'),
(1, 'Kepala Kantor Kementerian Agama Kabupaten Aceh Selatan', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Banda Aceh'),
(1, 'Kepala Kantor Kementerian Agama Kabupaten Aceh Singkil', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Banda Aceh'),
(1, 'Kepala Kantor Kementerian Agama Kabupaten Aceh Tengah', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Banda Aceh'),
(1, 'Kepala Kantor Kementerian Agama Kabupaten Aceh Tenggara', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Banda Aceh'),
(1, 'Kepala Kantor Kementerian Agama Kabupaten Aceh Timur', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Banda Aceh'),
(1, 'Kepala Kantor Kementerian Agama Kabupaten Aceh Utara', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Banda Aceh'),
(1, 'Kepala Kantor Kementerian Agama Kabupaten Bireuen', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Banda Aceh'),
(1, 'Kepala Kantor Kementerian Agama Kabupaten Pidie', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Banda Aceh'),
(1, 'Kepala Kantor Kementerian Agama Kabupaten Pidie Jaya', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Banda Aceh'),
(1, 'Kepala Kantor Kementerian Agama Kabupaten Simeuleue', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Banda Aceh'),
(1, 'Kepala Kantor Kementerian Agama Kabupaten Aceh Barat Daya', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Banda Aceh'),
(1, 'Kepala Kantor Kementerian Agama Kabupaten Aceh Jaya', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Banda Aceh'),
(1, 'Kepala Kantor Kementerian Agama Kabupaten Aceh Tamiang', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Banda Aceh'),
(1, 'Kepala Kantor Kementerian Agama Kabupaten Gayo Lues', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Banda Aceh'),
(1, 'Kepala Kantor Kementerian Agama Kabupaten Nagan Raya', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Banda Aceh'),
(1, 'Kepala Kantor Kementerian Agama Kabupaten Bener Meriah', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Banda Aceh'),
(1, 'Kepala Kantor Kementerian Agama Kota Banda Aceh', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Banda Aceh'),
(1, 'Kepala Kantor Kementerian Agama Kota Sabang', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Banda Aceh'),
(1, 'Kepala Kantor Kementerian Agama Kota Lhokseumawe', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Banda Aceh'),
(1, 'Kepala Kantor Kementerian Agama Kota Langsa', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Banda Aceh'),
(1, 'Kepala Kantor Kementerian Agama Kota Subulussalam', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Banda Aceh'),
(28, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Denpasar'),
(28, 'Kepala Kantor Kementerian Agama Kabupaten Badung', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Denpasar'),
(28, 'Kepala Kantor Kementerian Agama Kabupaten Bangli', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Denpasar'),
(28, 'Kepala Kantor Kementerian Agama Kabupaten Buleleng', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Denpasar'),
(28, 'Kepala Kantor Kementerian Agama Kabupaten Gianyar', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Denpasar'),
(28, 'Kepala Kantor Kementerian Agama Kabupaten Jembrana', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Denpasar'),
(28, 'Kepala Kantor Kementerian Agama Kabupaten Karangasem', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Denpasar'),
(28, 'Kepala Kantor Kementerian Agama Kabupaten Klungkung', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Denpasar'),
(28, 'Kepala Kantor Kementerian Agama Kabupaten Tabanan', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Denpasar'),
(28, 'Kepala Kantor Kementerian Agama Kota Denpasar', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Denpasar'),
(11, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Serang'),
(11, 'Kepala Kantor Kementerian Agama Kabupaten Lebak', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Serang'),
(11, 'Kepala Kantor Kementerian Agama Kabupaten Serang', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Serang'),
(11, 'Kepala Kantor Kementerian Agama Kabupaten Tangerang', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Serang'),
(11, 'Kepala Kantor Kementerian Agama Kabupaten Pandeglang', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Serang'),
(11, 'Kepala Kantor Kementerian Agama Kota Tangerang', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Serang'),
(11, 'Kepala Kantor Kementerian Agama Kota Cilegon', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Serang'),
(11, 'Kepala Kantor Kementerian Agama Kota Serang', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Serang'),
(11, 'Kepala Kantor Kementerian Agama Kota Tangerang Selatan', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Serang'),
(8, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Bengkulu'),
(8, 'Kepala Kantor Kementerian Agama Kabupaten Bengkulu Selatan', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Bengkulu'),
(8, 'Kepala Kantor Kementerian Agama Kabupaten Bengkulu Utara', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Bengkulu'),
(8, 'Kepala Kantor Kementerian Agama Kabupaten Bengkulu Tengah', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Bengkulu'),
(8, 'Kepala Kantor Kementerian Agama Kabupaten Benteng', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Bengkulu'),
(8, 'Kepala Kantor Kementerian Agama Kabupaten Rejang Lebong', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Bengkulu'),
(8, 'Kepala Kantor Kementerian Agama Kabupaten Kaur', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Bengkulu'),
(8, 'Kepala Kantor Kementerian Agama Kabupaten Mukomuko', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Bengkulu'),
(8, 'Kepala Kantor Kementerian Agama Kabupaten Seluma', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Bengkulu'),
(8, 'Kepala Kantor Kementerian Agama Kabupaten Kepahiang', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Bengkulu'),
(8, 'Kepala Kantor Kementerian Agama Kabupaten Lebong', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Bengkulu'),
(8, 'Kepala Kantor Kementerian Agama Kota Bengkulu', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Bengkulu'),
(15, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Yogyakarta'),
(15, 'Kepala Kantor Kementerian Agama Kabupaten Bantul', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Yogyakarta'),
(15, 'Kepala Kantor Kementerian Agama Kabupaten Gunungkidul', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Yogyakarta'),
(15, 'Kepala Kantor Kementerian Agama Kabupaten Kulon Progo', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Yogyakarta'),
(15, 'Kepala Kantor Kementerian Agama Kabupaten Sleman', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Yogyakarta'),
(15, 'Kepala Kantor Kementerian Agama Kota Yogyakarta', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Yogyakarta'),
(12, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Jakarta'),
(12, 'Kepala Kantor Kementerian Agama Kota Jakarta Barat', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Jakarta'),
(12, 'Kepala Kantor Kementerian Agama Kota Jakarta Pusat', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Jakarta'),
(12, 'Kepala Kantor Kementerian Agama Kota Jakarta Selatan', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Jakarta'),
(12, 'Kepala Kantor Kementerian Agama Kota Jakarta Timur', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Jakarta'),
(12, 'Kepala Kantor Kementerian Agama Kota Jakarta Utara', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Jakarta'),
(12, 'Kepala Kantor Kementerian Agama Kabupaten Kepulauan Seribu', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Jakarta'),
(23, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Gorontalo'),
(23, 'Kepala Kantor Kementerian Agama Kabupaten Boalemo', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Gorontalo'),
(23, 'Kepala Kantor Kementerian Agama Kabupaten Gorontalo', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Gorontalo'),
(23, 'Kepala Kantor Kementerian Agama Kabupaten Bone Bolango', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Gorontalo'),
(23, 'Kepala Kantor Kementerian Agama Kabupaten Pohuwato', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Gorontalo'),
(23, 'Kepala Kantor Kementerian Agama Kabupaten Gorontalo Utara', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Gorontalo'),
(23, 'Kepala Kantor Kementerian Agama Kota Gorontalo', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Gorontalo'),
(6, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Jambi'),
(6, 'Kepala Kantor Kementerian Agama Kabupaten Batanghari', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Jambi'),
(6, 'Kepala Kantor Kementerian Agama Kabupaten Bungo', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Jambi'),
(6, 'Kepala Kantor Kementerian Agama Kabupaten Kerinci', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Jambi'),
(6, 'Kepala Kantor Kementerian Agama Kabupaten Merangin', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Jambi'),
(6, 'Kepala Kantor Kementerian Agama Kabupaten Muaro Jambi', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Jambi'),
(6, 'Kepala Kantor Kementerian Agama Kabupaten Sarolangun', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Jambi'),
(6, 'Kepala Kantor Kementerian Agama Kabupaten Tanjung Jabung Barat', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Jambi'),
(6, 'Kepala Kantor Kementerian Agama Kabupaten Tanjung Jabung Timur', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Jambi'),
(6, 'Kepala Kantor Kementerian Agama Kabupaten Tebo', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Jambi'),
(6, 'Kepala Kantor Kementerian Agama Kota Jambi', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Jambi'),
(6, 'Kepala Kantor Kementerian Agama Kota Sungai Penuh', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Jambi'),
(13, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Bandung'),
(13, 'Kepala Kantor Kementerian Agama Kabupaten Bandung', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Bandung'),
(13, 'Kepala Kantor Kementerian Agama Kabupaten Bandung Barat', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Bandung'),
(13, 'Kepala Kantor Kementerian Agama Kabupaten Bekasi', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Bogor'),
(13, 'Kepala Kantor Kementerian Agama Kabupaten Bogor', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Bogor'),
(13, 'Kepala Kantor Kementerian Agama Kabupaten Ciamis', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Tasikmalaya'),
(13, 'Kepala Kantor Kementerian Agama Kabupaten Cianjur', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Bogor'),
(13, 'Kepala Kantor Kementerian Agama Kabupaten Cirebon', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Cirebon'),
(13, 'Kepala Kantor Kementerian Agama Kabupaten Garut', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Tasikmalaya'),
(13, 'Kepala Kantor Kementerian Agama Kabupaten Indramayu', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Cirebon'),
(13, 'Kepala Kantor Kementerian Agama Kabupaten Karawang', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Bogor'),
(13, 'Kepala Kantor Kementerian Agama Kabupaten Kuningan', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Cirebon'),
(13, 'Kepala Kantor Kementerian Agama Kabupaten Majalengka', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Cirebon'),
(13, 'Kepala Kantor Kementerian Agama Kabupaten Purwakarta', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Bandung'),
(13, 'Kepala Kantor Kementerian Agama Kabupaten Subang', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Bandung'),
(13, 'Kepala Kantor Kementerian Agama Kabupaten Sukabumi', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Bogor'),
(13, 'Kepala Kantor Kementerian Agama Kabupaten Sumedang', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Bandung'),
(13, 'Kepala Kantor Kementerian Agama Kabupaten Tasikmalaya', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Tasikmalaya'),
(13, 'Kepala Kantor Kementerian Agama Kota Bandung', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Bandung'),
(13, 'Kepala Kantor Kementerian Agama Kota Bekasi', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Bogor'),
(13, 'Kepala Kantor Kementerian Agama Kota Bogor', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Bogor'),
(13, 'Kepala Kantor Kementerian Agama Kota Cirebon', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Cirebon'),
(13, 'Kepala Kantor Kementerian Agama Kota Depok', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Bogor'),
(13, 'Kepala Kantor Kementerian Agama Kota Sukabumi', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Bogor'),
(13, 'Kepala Kantor Kementerian Agama Kota Tasikmalaya', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Tasikmalaya'),
(13, 'Kepala Kantor Kementerian Agama Kota Cimahi', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Bandung'),
(13, 'Kepala Kantor Kementerian Agama Kota Banjar', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Tasikmalaya'),
(14, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Semarang'),
(14, 'Kepala Kantor Kementerian Agama Kabupaten Banjarnegara', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Purwokerto'),
(14, 'Kepala Kantor Kementerian Agama Kabupaten Banyumas', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Purwokerto'),
(14, 'Kepala Kantor Kementerian Agama Kabupaten Batang', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Pekalongan'),
(14, 'Kepala Kantor Kementerian Agama Kabupaten Blora', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Semarang'),
(14, 'Kepala Kantor Kementerian Agama Kabupaten Boyolali', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Surakarta'),
(14, 'Kepala Kantor Kementerian Agama Kabupaten Brebes', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Pekalongan'),
(14, 'Kepala Kantor Kementerian Agama Kabupaten Cilacap', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Purwokerto'),
(14, 'Kepala Kantor Kementerian Agama Kabupaten Demak', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Semarang'),
(14, 'Kepala Kantor Kementerian Agama Kabupaten Grobogan', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Semarang'),
(14, 'Kepala Kantor Kementerian Agama Kabupaten Jepara', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Semarang'),
(14, 'Kepala Kantor Kementerian Agama Kabupaten Kebumen', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Purwokerto'),
(14, 'Kepala Kantor Kementerian Agama Kabupaten Kendal', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Semarang'),
(14, 'Kepala Kantor Kementerian Agama Kabupaten Klaten', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Surakarta'),
(14, 'Kepala Kantor Kementerian Agama Kabupaten Kudus', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Semarang'),
(14, 'Kepala Kantor Kementerian Agama Kabupaten Magelang', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Semarang'),
(14, 'Kepala Kantor Kementerian Agama Kabupaten Pati', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Semarang'),
(14, 'Kepala Kantor Kementerian Agama Kabupaten Pekalongan', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Pekalongan'),
(14, 'Kepala Kantor Kementerian Agama Kabupaten Pemalang', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Pekalongan'),
(14, 'Kepala Kantor Kementerian Agama Kabupaten Purbalingga', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Purwokerto'),
(14, 'Kepala Kantor Kementerian Agama Kabupaten Purworejo', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Purwokerto'),
(14, 'Kepala Kantor Kementerian Agama Kabupaten Rembang', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Semarang'),
(14, 'Kepala Kantor Kementerian Agama Kabupaten Semarang', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Semarang'),
(14, 'Kepala Kantor Kementerian Agama Kabupaten Sragen', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Surakarta'),
(14, 'Kepala Kantor Kementerian Agama Kabupaten Sukoharjo', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Surakarta'),
(14, 'Kepala Kantor Kementerian Agama Kabupaten Tegal', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Pekalongan'),
(14, 'Kepala Kantor Kementerian Agama Kabupaten Temanggung', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Semarang'),
(14, 'Kepala Kantor Kementerian Agama Kabupaten Wonogiri', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Surakarta'),
(14, 'Kepala Kantor Kementerian Agama Kabupaten Wonosobo', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Purwokerto'),
(14, 'Kepala Kantor Kementerian Agama Kabupaten Karanganyar', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Surakarta'),
(14, 'Kepala Kantor Kementerian Agama Kota Semarang', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Semarang'),
(14, 'Kepala Kantor Kementerian Agama Kota Salatiga', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Semarang'),
(14, 'Kepala Kantor Kementerian Agama Kota Surakarta', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Surakarta'),
(14, 'Kepala Kantor Kementerian Agama Kota Tegal', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Pekalongan'),
(14, 'Kepala Kantor Kementerian Agama Kota Magelang', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Semarang'),
(14, 'Kepala Kantor Kementerian Agama Kota Pekalongan', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Pekalongan'),
(16, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Surabaya'),
(16, 'Kepala Kantor Kementerian Agama Kabupaten Bangkalan', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Surabaya'),
(16, 'Kepala Kantor Kementerian Agama Kabupaten Banyuwangi', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Jember'),
(16, 'Kepala Kantor Kementerian Agama Kabupaten Blitar', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Kediri'),
(16, 'Kepala Kantor Kementerian Agama Kabupaten Bojonegoro', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Surabaya'),
(16, 'Kepala Kantor Kementerian Agama Kabupaten Bondowoso', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Jember'),
(16, 'Kepala Kantor Kementerian Agama Kabupaten Gresik', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Surabaya'),
(16, 'Kepala Kantor Kementerian Agama Kabupaten Jember', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Jember'),
(16, 'Kepala Kantor Kementerian Agama Kabupaten Jombang', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Surabaya'),
(16, 'Kepala Kantor Kementerian Agama Kabupaten Kediri', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Kediri'),
(16, 'Kepala Kantor Kementerian Agama Kabupaten Lamongan', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Surabaya'),
(16, 'Kepala Kantor Kementerian Agama Kabupaten Lumajang', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Malang'),
(16, 'Kepala Kantor Kementerian Agama Kabupaten Madiun', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Madiun'),
(16, 'Kepala Kantor Kementerian Agama Kabupaten Magetan', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Madiun'),
(16, 'Kepala Kantor Kementerian Agama Kabupaten Malang', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Malang'),
(16, 'Kepala Kantor Kementerian Agama Kabupaten Mojokerto', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Surabaya'),
(16, 'Kepala Kantor Kementerian Agama Kabupaten Nganjuk', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Kediri'),
(16, 'Kepala Kantor Kementerian Agama Kabupaten Ngawi', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Madiun'),
(16, 'Kepala Kantor Kementerian Agama Kabupaten Pacitan', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Madiun'),
(16, 'Kepala Kantor Kementerian Agama Kabupaten Pamekasan', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Surabaya'),
(16, 'Kepala Kantor Kementerian Agama Kabupaten Pasuruan', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Malang'),
(16, 'Kepala Kantor Kementerian Agama Kabupaten Ponorogo', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Madiun'),
(16, 'Kepala Kantor Kementerian Agama Kabupaten Probolinggo', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Malang'),
(16, 'Kepala Kantor Kementerian Agama Kabupaten Sampang', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Surabaya'),
(16, 'Kepala Kantor Kementerian Agama Kabupaten Sidoarjo', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Surabaya'),
(16, 'Kepala Kantor Kementerian Agama Kabupaten Situbondo', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Jember'),
(16, 'Kepala Kantor Kementerian Agama Kabupaten Sumenep', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Surabaya'),
(16, 'Kepala Kantor Kementerian Agama Kabupaten Trenggalek', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Kediri'),
(16, 'Kepala Kantor Kementerian Agama Kabupaten Tuban', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Surabaya'),
(16, 'Kepala Kantor Kementerian Agama Kabupaten Tulungagung', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Kediri'),
(16, 'Kepala Kantor Kementerian Agama Kota Surabaya', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Surabaya'),
(16, 'Kepala Kantor Kementerian Agama Kota Blitar', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Kediri'),
(16, 'Kepala Kantor Kementerian Agama Kota Kediri', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Kediri'),
(16, 'Kepala Kantor Kementerian Agama Kota Madiun', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Madiun'),
(16, 'Kepala Kantor Kementerian Agama Kota Malang', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Malang'),
(16, 'Kepala Kantor Kementerian Agama Kota Mojokerto', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Surabaya'),
(16, 'Kepala Kantor Kementerian Agama Kota Pasuruan', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Malang'),
(16, 'Kepala Kantor Kementerian Agama Kota Batu', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Malang'),
(16, 'Kepala Kantor Kementerian Agama Kota Probolinggo', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Malang'),
(17, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Pontianak'),
(17, 'Kepala Kantor Kementerian Agama Kabupaten Bengkayang', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Pontianak'),
(17, 'Kepala Kantor Kementerian Agama Kabupaten Kapuas Hulu', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Pontianak'),
(17, 'Kepala Kantor Kementerian Agama Kabupaten Ketapang', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Pontianak'),
(17, 'Kepala Kantor Kementerian Agama Kabupaten Pontianak', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Pontianak'),
(17, 'Kepala Kantor Kementerian Agama Kabupaten Sambas', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Pontianak'),
(17, 'Kepala Kantor Kementerian Agama Kabupaten Sanggau', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Pontianak'),
(17, 'Kepala Kantor Kementerian Agama Kabupaten Sintang', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Pontianak'),
(17, 'Kepala Kantor Kementerian Agama Kabupaten Landak', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Pontianak'),
(17, 'Kepala Kantor Kementerian Agama Kabupaten Melawi', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Pontianak'),
(17, 'Kepala Kantor Kementerian Agama Kabupaten Sekadau', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Pontianak'),
(17, 'Kepala Kantor Kementerian Agama Kabupaten Kayong Utara', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Pontianak'),
(17, 'Kepala Kantor Kementerian Agama Kabupaten Kubu Raya', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Pontianak'),
(17, 'Kepala Kantor Kementerian Agama Kota Pontianak', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Pontianak'),
(17, 'Kepala Kantor Kementerian Agama Kota Singkawang', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Pontianak'),
(19, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Banjarmasin'),
(19, 'Kepala Kantor Kementerian Agama Kabupaten Banjar', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Banjarmasin'),
(19, 'Kepala Kantor Kementerian Agama Kabupaten Barito Kuala', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Banjarmasin'),
(19, 'Kepala Kantor Kementerian Agama Kabupaten Hulu Sungai Selatan', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Banjarmasin'),
(19, 'Kepala Kantor Kementerian Agama Kabupaten Hulu Sungai Tengah', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Banjarmasin'),
(19, 'Kepala Kantor Kementerian Agama Kabupaten Hulu Sungai Utara', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Banjarmasin'),
(19, 'Kepala Kantor Kementerian Agama Kabupaten Kotabaru', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Banjarmasin'),
(19, 'Kepala Kantor Kementerian Agama Kabupaten Tabalong', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Banjarmasin'),
(19, 'Kepala Kantor Kementerian Agama Kabupaten Tanah Laut', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Banjarmasin'),
(19, 'Kepala Kantor Kementerian Agama Kabupaten Tapin', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Banjarmasin'),
(19, 'Kepala Kantor Kementerian Agama Kabupaten Tanah Bumbu', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Banjarmasin'),
(19, 'Kepala Kantor Kementerian Agama Kabupaten Balangan', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Banjarmasin'),
(19, 'Kepala Kantor Kementerian Agama Kota Banjarmasin', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Banjarmasin'),
(19, 'Kepala Kantor Kementerian Agama Kota Banjarbaru', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Banjarmasin'),
(18, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Palangka Raya'),
(18, 'Kepala Kantor Kementerian Agama Kabupaten Barito Selatan', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Palangka Raya'),
(18, 'Kepala Kantor Kementerian Agama Kabupaten Barito Utara', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Palangka Raya'),
(18, 'Kepala Kantor Kementerian Agama Kabupaten Kapuas', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Palangka Raya'),
(18, 'Kepala Kantor Kementerian Agama Kabupaten Kotawaringin Barat', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Palangka Raya'),
(18, 'Kepala Kantor Kementerian Agama Kabupaten Kotawaringin Timur', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Palangka Raya'),
(18, 'Kepala Kantor Kementerian Agama Kabupaten Barito Timur', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Palangka Raya'),
(18, 'Kepala Kantor Kementerian Agama Kabupaten Gunung Mas', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Palangka Raya'),
(18, 'Kepala Kantor Kementerian Agama Kabupaten Katingan', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Palangka Raya'),
(18, 'Kepala Kantor Kementerian Agama Kabupaten Lamandau', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Palangka Raya'),
(18, 'Kepala Kantor Kementerian Agama Kabupaten Murung Raya', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Palangka Raya'),
(18, 'Kepala Kantor Kementerian Agama Kabupaten Pulang Pisau', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Palangka Raya'),
(18, 'Kepala Kantor Kementerian Agama Kabupaten Seruyan', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Palangka Raya'),
(18, 'Kepala Kantor Kementerian Agama Kabupaten Sukamara', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Palangka Raya'),
(18, 'Kepala Kantor Kementerian Agama Kabupaten Kasongan', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Palangka Raya'),
(18, 'Kepala Kantor Kementerian Agama Kota Palangka Raya', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Palangka Raya'),
(20, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Samarinda'),
(20, 'Kepala Kantor Kementerian Agama Kabupaten Berau', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Samarinda'),
(20, 'Kepala Kantor Kementerian Agama Kabupaten Bulungan', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Samarinda'),
(20, 'Kepala Kantor Kementerian Agama Kabupaten Kutai Kertanegara', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Samarinda'),
(20, 'Kepala Kantor Kementerian Agama Kabupaten Kutai Barat', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Samarinda'),
(20, 'Kepala Kantor Kementerian Agama Kabupaten Kutai Timur', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Samarinda'),
(20, 'Kepala Kantor Kementerian Agama Kabupaten Malinau', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Samarinda'),
(20, 'Kepala Kantor Kementerian Agama Kabupaten Nunukan', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Samarinda'),
(20, 'Kepala Kantor Kementerian Agama Kabupaten Pasir', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Samarinda'),
(20, 'Kepala Kantor Kementerian Agama Kabupaten Penajam Paser Utara', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Samarinda'),
(20, 'Kepala Kantor Kementerian Agama Kabupaten Tana Tidung', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Samarinda'),
(20, 'Kepala Kantor Kementerian Agama Kota Samarinda', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Samarinda'),
(20, 'Kepala Kantor Kementerian Agama Kota Balikpapan', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Samarinda'),
(20, 'Kepala Kantor Kementerian Agama Kota Bontang', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Samarinda'),
(20, 'Kepala Kantor Kementerian Agama Kota Tarakan', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Samarinda'),
(10, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Pangkalpinang'),
(10, 'Kepala Kantor Kementerian Agama Kabupaten Bangka', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Pangkalpinang'),
(10, 'Kepala Kantor Kementerian Agama Kabupaten Belitung', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Pangkalpinang'),
(10, 'Kepala Kantor Kementerian Agama Kota Pangkalpinang', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Pangkalpinang'),
(10, 'Kepala Kantor Kementerian Agama Kabupaten Bangka Barat', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Pangkalpinang'),
(10, 'Kepala Kantor Kementerian Agama Kabupaten Bangka Selatan', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Pangkalpinang'),
(10, 'Kepala Kantor Kementerian Agama Kabupaten Belitung Timur', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Pangkalpinang'),
(10, 'Kepala Kantor Kementerian Agama Kabupaten Bangka Tengah', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Pangkalpinang'),
(5, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Tanjungpinang'),
(5, 'Kepala Kantor Kementerian Agama Kabupaten Anambas', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Tanjungpinang'),
(5, 'Kepala Kantor Kementerian Agama Kabupaten Karimun', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Tanjungpinang'),
(5, 'Kepala Kantor Kementerian Agama Kabupaten Natuna', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Tanjungpinang'),
(5, 'Kepala Kantor Kementerian Agama Kabupaten Lingga', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Tanjungpinang'),
(5, 'Kepala Kantor Kementerian Agama Kabupaten Bintan', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Tanjungpinang'),
(5, 'Kepala Kantor Kementerian Agama Kota Tanjungpinang', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Tanjungpinang'),
(5, 'Kepala Kantor Kementerian Agama Kota Batam', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Tanjungpinang'),
(9, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Lampung'),
(9, 'Kepala Kantor Kementerian Agama Kabupaten Lampung Barat', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Lampung'),
(9, 'Kepala Kantor Kementerian Agama Kabupaten Lampung Selatan', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Lampung'),
(9, 'Kepala Kantor Kementerian Agama Kabupaten Lampung Tengah', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Lampung'),
(9, 'Kepala Kantor Kementerian Agama Kabupaten Lampung Timur', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Lampung'),
(9, 'Kepala Kantor Kementerian Agama Kabupaten Lampung Utara', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Lampung'),
(9, 'Kepala Kantor Kementerian Agama Kabupaten Tanggamus', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Lampung'),
(9, 'Kepala Kantor Kementerian Agama Kabupaten Tulang Bawang', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Lampung'),
(9, 'Kepala Kantor Kementerian Agama Kabupaten Tulang Bawang Barat', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Lampung'),
(9, 'Kepala Kantor Kementerian Agama Kabupaten Way Kanan', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Lampung'),
(9, 'Kepala Kantor Kementerian Agama Kabupaten Pesawaran', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Lampung'),
(9, 'Kepala Kantor Kementerian Agama Kabupaten Mesuji', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Lampung'),
(9, 'Kepala Kantor Kementerian Agama Kabupaten Pringsewu', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Lampung'),
(9, 'Kepala Kantor Kementerian Agama Kota Bandar Lampung', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Lampung'),
(9, 'Kepala Kantor Kementerian Agama Kota Metro', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Lampung'),
(31, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Ambon'),
(31, 'Kepala Kantor Kementerian Agama Kabupaten Maluku Tengah', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Ambon'),
(31, 'Kepala Kantor Kementerian Agama Kabupaten Maluku Tenggara', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Ambon'),
(31, 'Kepala Kantor Kementerian Agama Kabupaten Buru', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Ambon'),
(31, 'Kepala Kantor Kementerian Agama Kabupaten Maluku Tenggara Barat', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Ambon'),
(31, 'Kepala Kantor Kementerian Agama Kabupaten Kepulauan Aru', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Ambon'),
(31, 'Kepala Kantor Kementerian Agama Kabupaten Buru Selatan', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Ambon'),
(31, 'Kepala Kantor Kementerian Agama Kabupaten Maluku Barat Daya', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Ambon'),
(31, 'Kepala Kantor Kementerian Agama Kabupaten Seram Bagian Barat', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Ambon'),
(31, 'Kepala Kantor Kementerian Agama Kabupaten Seram Bagian Timur', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Ambon'),
(31, 'Kepala Kantor Kementerian Agama Kota Ambon', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Ambon'),
(31, 'Kepala Kantor Kementerian Agama Kota Tual', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Ambon'),
(32, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Ternate'),
(32, 'Kepala Kantor Kementerian Agama Kabupaten Halmahera Barat', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Ternate'),
(32, 'Kepala Kantor Kementerian Agama Kabupaten Halmahera Timur', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Ternate'),
(32, 'Kepala Kantor Kementerian Agama Kabupaten Halmahera Utara', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Ternate'),
(32, 'Kepala Kantor Kementerian Agama Kabupaten Halmahera Selatan', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Ternate'),
(32, 'Kepala Kantor Kementerian Agama Kabupaten Kepulauan Sula', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Ternate'),
(32, 'Kepala Kantor Kementerian Agama Kabupaten Halmahera Tengah', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Ternate'),
(32, 'Kepala Kantor Kementerian Agama Kabupaten Pulau Morotai', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Ternate'),
(32, 'Kepala Kantor Kementerian Agama Kota Ternate', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Ternate'),
(32, 'Kepala Kantor Kementerian Agama Kota Tidore Kepulauan', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Ternate'),
(29, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Mataram'),
(29, 'Kepala Kantor Kementerian Agama Kabupaten Bima', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Mataram'),
(29, 'Kepala Kantor Kementerian Agama Kabupaten Dompu', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Mataram'),
(29, 'Kepala Kantor Kementerian Agama Kabupaten Lombok Barat', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Mataram'),
(29, 'Kepala Kantor Kementerian Agama Kabupaten Lombok Tengah', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Mataram'),
(29, 'Kepala Kantor Kementerian Agama Kabupaten Lombok Timur', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Mataram'),
(29, 'Kepala Kantor Kementerian Agama Kabupaten Lombok Utara', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Mataram'),
(29, 'Kepala Kantor Kementerian Agama Kabupaten Sumbawa', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Mataram'),
(29, 'Kepala Kantor Kementerian Agama Kabupaten Sumbawa Barat', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Mataram'),
(29, 'Kepala Kantor Kementerian Agama Kota Mataram', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Mataram'),
(29, 'Kepala Kantor Kementerian Agama Kota Bima', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Mataram'),
(30, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Kupang'),
(30, 'Kepala Kantor Kementerian Agama Kabupaten Alor', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Kupang'),
(30, 'Kepala Kantor Kementerian Agama Kabupaten Belu', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Kupang'),
(30, 'Kepala Kantor Kementerian Agama Kabupaten Ende', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Kupang'),
(30, 'Kepala Kantor Kementerian Agama Kabupaten Flores Timur', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Kupang'),
(30, 'Kepala Kantor Kementerian Agama Kabupaten Kupang', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Kupang'),
(30, 'Kepala Kantor Kementerian Agama Kabupaten Lembata', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Kupang'),
(30, 'Kepala Kantor Kementerian Agama Kabupaten Manggarai', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Kupang'),
(30, 'Kepala Kantor Kementerian Agama Kabupaten Ngada', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Kupang'),
(30, 'Kepala Kantor Kementerian Agama Kabupaten Sikka', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Kupang'),
(30, 'Kepala Kantor Kementerian Agama Kabupaten Sumba Barat', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Kupang'),
(30, 'Kepala Kantor Kementerian Agama Kabupaten Sumba Timur', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Kupang'),
(30, 'Kepala Kantor Kementerian Agama Kabupaten Timor Tengah Selatan', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Kupang'),
(30, 'Kepala Kantor Kementerian Agama Kabupaten Timor Tengah Utara', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Kupang'),
(30, 'Kepala Kantor Kementerian Agama Kabupaten Rote Ndao', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Kupang'),
(30, 'Kepala Kantor Kementerian Agama Kabupaten Manggarai Barat', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Kupang'),
(30, 'Kepala Kantor Kementerian Agama Kabupaten Kabupaten Nagekeo', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Kupang');
INSERT INTO `tbl_taspen` (`KODE_SATUAN_KERJA`, `Tembusan`, `Taspen`) VALUES
(30, 'Kepala Kantor Kementerian Agama Kabupaten Kabupaten Sumba Tengah', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Kupang'),
(30, 'Kepala Kantor Kementerian Agama Kabupaten Kabupaten Sumba Barat Daya', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Kupang'),
(30, 'Kepala Kantor Kementerian Agama Kabupaten Kabupaten Manggarai Timur', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Kupang'),
(30, 'Kepala Kantor Kementerian Agama Kabupaten Kabupaten Sabu Raijua', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Kupang'),
(30, 'Kepala Kantor Kementerian Agama Kota Kupang', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Kupang'),
(33, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Jayapura'),
(33, 'Kepala Kantor Kementerian Agama Kabupaten Jayapura', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Jayapura'),
(33, 'Kepala Kantor Kementerian Agama Kabupaten Puncak Jaya', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Jayapura'),
(33, 'Kepala Kantor Kementerian Agama Kabupaten Merauke', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Jayapura'),
(33, 'Kepala Kantor Kementerian Agama Kabupaten Mimika', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Jayapura'),
(33, 'Kepala Kantor Kementerian Agama Kabupaten Nabire', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Jayapura'),
(33, 'Kepala Kantor Kementerian Agama Kabupaten Supiori', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Jayapura'),
(33, 'Kepala Kantor Kementerian Agama Kabupaten Waropen', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Jayapura'),
(33, 'Kepala Kantor Kementerian Agama Kabupaten Kepulauan Yapen', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Jayapura'),
(33, 'Kepala Kantor Kementerian Agama Kabupaten Biak Numfor', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Jayapura'),
(33, 'Kepala Kantor Kementerian Agama Kabupaten Paniai', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Jayapura'),
(33, 'Kepala Kantor Kementerian Agama Kabupaten Jayawijaya', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Jayapura'),
(33, 'Kepala Kantor Kementerian Agama Kabupaten Asmat', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Jayapura'),
(33, 'Kepala Kantor Kementerian Agama Kabupaten Boven Digoel', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Jayapura'),
(33, 'Kepala Kantor Kementerian Agama Kabupaten Keerom', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Jayapura'),
(33, 'Kepala Kantor Kementerian Agama Kabupaten Mappi', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Jayapura'),
(33, 'Kepala Kantor Kementerian Agama Kabupaten Pengunungan Bintang', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Jayapura'),
(33, 'Kepala Kantor Kementerian Agama Kabupaten Sarmi', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Jayapura'),
(33, 'Kepala Kantor Kementerian Agama Kabupaten Tolikara', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Jayapura'),
(33, 'Kepala Kantor Kementerian Agama Kabupaten Yahukimo', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Jayapura'),
(33, 'Kepala Kantor Kementerian Agama Kabupaten Deiyai', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Jayapura'),
(33, 'Kepala Kantor Kementerian Agama Kabupaten Dogiyai', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Jayapura'),
(33, 'Kepala Kantor Kementerian Agama Kabupaten Intan Jaya', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Jayapura'),
(33, 'Kepala Kantor Kementerian Agama Kabupaten Lanny Jaya', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Jayapura'),
(33, 'Kepala Kantor Kementerian Agama Kabupaten Mamberamo Raya', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Jayapura'),
(33, 'Kepala Kantor Kementerian Agama Kabupaten Mamberamo Tengah', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Jayapura'),
(33, 'Kepala Kantor Kementerian Agama Kabupaten Nduga', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Jayapura'),
(33, 'Kepala Kantor Kementerian Agama Kabupaten Puncak', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Jayapura'),
(33, 'Kepala Kantor Kementerian Agama Kabupaten Yalimo', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Jayapura'),
(33, 'Kepala Kantor Kementerian Agama Kota Jayapura', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Jayapura'),
(34, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Manokwari'),
(34, 'Kepala Kantor Kementerian Agama Kabupaten Sorong', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Manokwari'),
(34, 'Kepala Kantor Kementerian Agama Kabupaten Manokwari', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Manokwari'),
(34, 'Kepala Kantor Kementerian Agama Kabupaten Fak-Fak', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Manokwari'),
(34, 'Kepala Kantor Kementerian Agama Kabupaten Sorong Selatan', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Manokwari'),
(34, 'Kepala Kantor Kementerian Agama Kabupaten Raja Ampat', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Manokwari'),
(34, 'Kepala Kantor Kementerian Agama Kabupaten Teluk Bentuni', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Manokwari'),
(34, 'Kepala Kantor Kementerian Agama Kabupaten Teluk Wondama', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Manokwari'),
(34, 'Kepala Kantor Kementerian Agama Kabupaten Kaimana', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Manokwari'),
(34, 'Kepala Kantor Kementerian Agama Kabupaten Maybrat', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Manokwari'),
(34, 'Kepala Kantor Kementerian Agama Kabupaten Tambrauw', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Manokwari'),
(34, 'Kepala Kantor Kementerian Agama Kota Sorong', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Manokwari'),
(4, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Pekanbaru'),
(4, 'Kepala Kantor Kementerian Agama Kabupaten Bengkalis', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Pekanbaru'),
(4, 'Kepala Kantor Kementerian Agama Kabupaten Indragiri Hilir', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Pekanbaru'),
(4, 'Kepala Kantor Kementerian Agama Kabupaten Indragiri Hulu', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Pekanbaru'),
(4, 'Kepala Kantor Kementerian Agama Kabupaten Kampar', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Pekanbaru'),
(4, 'Kepala Kantor Kementerian Agama Kabupaten Kuantan Sengingi', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Pekanbaru'),
(4, 'Kepala Kantor Kementerian Agama Kabupaten Pelalawan', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Pekanbaru'),
(4, 'Kepala Kantor Kementerian Agama Kabupaten Rokan Hilir', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Pekanbaru'),
(4, 'Kepala Kantor Kementerian Agama Kabupaten Rokan Hulu', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Pekanbaru'),
(4, 'Kepala Kantor Kementerian Agama Kabupaten Siak', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Pekanbaru'),
(4, 'Kepala Kantor Kementerian Agama Kepulauan Meranti', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Pekanbaru'),
(4, 'Kepala Kantor Kementerian Agama Kota Dumai', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Pekanbaru'),
(4, 'Kepala Kantor Kementerian Agama Kota Pekanbaru', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Pekanbaru'),
(26, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Mamuju'),
(26, 'Kepala Kantor Kementerian Agama Kabupaten Polewali Mamasa', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Mamuju'),
(26, 'Kepala Kantor Kementerian Agama Kabupaten Majene', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Mamuju'),
(26, 'Kepala Kantor Kementerian Agama Kabupaten Mamuju', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Mamuju'),
(26, 'Kepala Kantor Kementerian Agama Kabupaten Mamasa', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Mamuju'),
(26, 'Kepala Kantor Kementerian Agama Kabupaten Mamuju Utara', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Mamuju'),
(26, 'Kepala Kantor Kementerian Agama Kabupaten Polewali Mandar', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Mamuju'),
(27, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Makassar'),
(27, 'Kepala Kantor Kementerian Agama Kabupaten Bantaeng', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Makassar'),
(27, 'Kepala Kantor Kementerian Agama Kabupaten Barru', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Makassar'),
(27, 'Kepala Kantor Kementerian Agama Kabupaten Bone', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Makassar'),
(27, 'Kepala Kantor Kementerian Agama Kabupaten Bulukumba', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Makassar'),
(27, 'Kepala Kantor Kementerian Agama Kabupaten Enrekang', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Makassar'),
(27, 'Kepala Kantor Kementerian Agama Kabupaten Gowa', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Makassar'),
(27, 'Kepala Kantor Kementerian Agama Kabupaten Jeneponto', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Makassar'),
(27, 'Kepala Kantor Kementerian Agama Kabupaten Luwu', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Makassar'),
(27, 'Kepala Kantor Kementerian Agama Kabupaten Luwu Utara', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Makassar'),
(27, 'Kepala Kantor Kementerian Agama Kabupaten Luwu Timur', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Makassar'),
(27, 'Kepala Kantor Kementerian Agama Kabupaten Maros', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Makassar'),
(27, 'Kepala Kantor Kementerian Agama Kabupaten Pangkajene Kepulauan', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Makassar'),
(27, 'Kepala Kantor Kementerian Agama Kabupaten Pinrang', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Makassar'),
(27, 'Kepala Kantor Kementerian Agama Kabupaten Selayar', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Makassar'),
(27, 'Kepala Kantor Kementerian Agama Kabupaten Sidenreng Rappang', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Makassar'),
(27, 'Kepala Kantor Kementerian Agama Kabupaten Sinjai', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Makassar'),
(27, 'Kepala Kantor Kementerian Agama Kabupaten Soppeng', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Makassar'),
(27, 'Kepala Kantor Kementerian Agama Kabupaten Takalar', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Makassar'),
(27, 'Kepala Kantor Kementerian Agama Kabupaten Tana Toraja', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Makassar'),
(27, 'Kepala Kantor Kementerian Agama Kabupaten Toraja Utara', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Makassar'),
(27, 'Kepala Kantor Kementerian Agama Kabupaten Wajo', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Makassar'),
(27, 'Kepala Kantor Kementerian Agama Kabupaten Sengkang', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Makassar'),
(27, 'Kepala Kantor Kementerian Agama Kota Makassar', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Makassar'),
(27, 'Kepala Kantor Kementerian Agama Kota Parepare', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Makassar'),
(27, 'Kepala Kantor Kementerian Agama Kota Palopo', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Makassar'),
(24, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Palu'),
(24, 'Kepala Kantor Kementerian Agama Kabupaten Banggai', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Palu'),
(24, 'Kepala Kantor Kementerian Agama Kabupaten Banggai Kepulauan', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Palu'),
(24, 'Kepala Kantor Kementerian Agama Kabupaten Buol', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Palu'),
(24, 'Kepala Kantor Kementerian Agama Kabupaten Donggala', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Palu'),
(24, 'Kepala Kantor Kementerian Agama Kabupaten Morowali', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Palu'),
(24, 'Kepala Kantor Kementerian Agama Kabupaten Poso', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Palu'),
(24, 'Kepala Kantor Kementerian Agama Kabupaten Toli-Toli', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Palu'),
(24, 'Kepala Kantor Kementerian Agama Kabupaten Parigi Moutong', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Palu'),
(24, 'Kepala Kantor Kementerian Agama Kabupaten Sigi', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Palu'),
(24, 'Kepala Kantor Kementerian Agama Kabupaten Tojo Una-Una', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Palu'),
(24, 'Kepala Kantor Kementerian Agama Kota Palu', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Palu'),
(25, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Kendari'),
(25, 'Kepala Kantor Kementerian Agama Kabupaten Kolaka', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Kendari'),
(25, 'Kepala Kantor Kementerian Agama Kabupaten Konawe', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Kendari'),
(25, 'Kepala Kantor Kementerian Agama Kabupaten Muna', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Kendari'),
(25, 'Kepala Kantor Kementerian Agama Kabupaten Buton', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Kendari'),
(25, 'Kepala Kantor Kementerian Agama Kabupaten Buton Utara', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Kendari'),
(25, 'Kepala Kantor Kementerian Agama Kabupaten Konawe Selatan', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Kendari'),
(25, 'Kepala Kantor Kementerian Agama Kabupaten Wakatobi', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Kendari'),
(25, 'Kepala Kantor Kementerian Agama Kabupaten Bombana', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Kendari'),
(25, 'Kepala Kantor Kementerian Agama Kabupaten Kolaka Utara', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Kendari'),
(25, 'Kepala Kantor Kementerian Agama Kabupaten Kendari', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Kendari'),
(25, 'Kepala Kantor Kementerian Agama Kota Kendari', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Kendari'),
(25, 'Kepala Kantor Kementerian Agama Kota Bau-bau', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Kendari'),
(22, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Manado'),
(22, 'Kepala Kantor Kementerian Agama Kabupaten Bolaang Mongondow', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Manado'),
(22, 'Kepala Kantor Kementerian Agama Kabupaten Bolaang Mangondow Selatan', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Manado'),
(22, 'Kepala Kantor Kementerian Agama Kabupaten Bolaang Mangondow Timur', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Manado'),
(22, 'Kepala Kantor Kementerian Agama Kabupaten Bolaang Mangondow Utara', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Manado'),
(22, 'Kepala Kantor Kementerian Agama Kabupaten Minahasa', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Manado'),
(22, 'Kepala Kantor Kementerian Agama Kabupaten Minahasa Utara', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Manado'),
(22, 'Kepala Kantor Kementerian Agama Kabupaten Minahasa Selatan', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Manado'),
(22, 'Kepala Kantor Kementerian Agama Kabupaten Minahasa Tenggara', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Manado'),
(22, 'Kepala Kantor Kementerian Agama Kabupaten Kepulauan Sangihe', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Manado'),
(22, 'Kepala Kantor Kementerian Agama Kabupaten Kepulauan Talaud', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Manado'),
(22, 'Kepala Kantor Kementerian Agama Kabupaten Kepulauan Sitaro', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Manado'),
(22, 'Kepala Kantor Kementerian Agama Kota Kotamobagu', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Manado'),
(22, 'Kepala Kantor Kementerian Agama Kota Manado', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Manado'),
(22, 'Kepala Kantor Kementerian Agama Kota Bitung', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Manado'),
(22, 'Kepala Kantor Kementerian Agama Kota Tomohon', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Manado'),
(3, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Padang'),
(3, 'Kepala Kantor Kementerian Agama Kabupaten Limapuluh Kota', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Bukittinggi'),
(3, 'Kepala Kantor Kementerian Agama Kabupaten Agam', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Bukittinggi'),
(3, 'Kepala Kantor Kementerian Agama Kabupaten Padang Pariaman', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Padang'),
(3, 'Kepala Kantor Kementerian Agama Kabupaten Pasaman', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Bukittinggi'),
(3, 'Kepala Kantor Kementerian Agama Kabupaten Pesisir Selatan', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Padang'),
(3, 'Kepala Kantor Kementerian Agama Kabupaten Sawahlunto/Sijunjung', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Padang'),
(3, 'Kepala Kantor Kementerian Agama Kabupaten Solok', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Padang'),
(3, 'Kepala Kantor Kementerian Agama Kabupaten Tanah Datar', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Bukittinggi'),
(3, 'Kepala Kantor Kementerian Agama Kabupaten Kepulauan Mentawai', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Padang'),
(3, 'Kepala Kantor Kementerian Agama Kabupaten Pasaman Barat', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Bukittinggi'),
(3, 'Kepala Kantor Kementerian Agama Kabupaten Solok Selatan', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Padang'),
(3, 'Kepala Kantor Kementerian Agama Kabupaten Dharmasraya', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Padang'),
(3, 'Kepala Kantor Kementerian Agama Kota Padang', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Padang'),
(3, 'Kepala Kantor Kementerian Agama Kota Bukittinggi', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Bukittinggi'),
(3, 'Kepala Kantor Kementerian Agama Kota Padang Panjang', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Bukittinggi'),
(3, 'Kepala Kantor Kementerian Agama Kota Payakumbuh', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Bukittinggi'),
(3, 'Kepala Kantor Kementerian Agama Kota Sawahlunto', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Padang'),
(3, 'Kepala Kantor Kementerian Agama Kota Solok', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Padang'),
(3, 'Kepala Kantor Kementerian Agama Kota Pariaman', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Padang'),
(7, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Palembang'),
(7, 'Kepala Kantor Kementerian Agama Kabupaten Lahat', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Lubuk Linggau'),
(7, 'Kepala Kantor Kementerian Agama Kabupaten Muara Enim', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Lubuk Linggau'),
(7, 'Kepala Kantor Kementerian Agama Kabupaten Musi Banyuasin', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Palembang'),
(7, 'Kepala Kantor Kementerian Agama Kabupaten Musi Rawas', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Lubuk Linggau'),
(7, 'Kepala Kantor Kementerian Agama Kabupaten Musi Rawas Utara', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Lubuk Linggau'),
(7, 'Kepala Kantor Kementerian Agama Kabupaten Ogan Komering Ilir', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Palembang'),
(7, 'Kepala Kantor Kementerian Agama Kabupaten Ogan Komering Ulu', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Palembang'),
(7, 'Kepala Kantor Kementerian Agama Kabupaten Banyuasin', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Palembang'),
(7, 'Kepala Kantor Kementerian Agama Kabupaten OKU Timur', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Palembang'),
(7, 'Kepala Kantor Kementerian Agama Kabupaten OKU Selatan', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Palembang'),
(7, 'Kepala Kantor Kementerian Agama Kabupaten Ogan Ilir', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Palembang'),
(7, 'Kepala Kantor Kementerian Agama Kabupaten Empat Lawang', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Lubuk Linggau'),
(7, 'Kepala Kantor Kementerian Agama Kota Palembang', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Palembang'),
(7, 'Kepala Kantor Kementerian Agama Kota Pagar Alam', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Lubuk Linggau'),
(7, 'Kepala Kantor Kementerian Agama Kota Lubuk Linggau', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Lubuk Linggau'),
(7, 'Kepala Kantor Kementerian Agama Kota Prabumulih', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Palembang'),
(7, 'Kepala Kantor Kementerian Agama Kabupaten Penukang Abad Lematang Ilir', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Lubuk Linggau'),
(2, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Medan'),
(2, 'Kepala Kantor Kementerian Agama Kabupaten Asahan', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Medan'),
(2, 'Kepala Kantor Kementerian Agama Kabupaten Dairi', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Pematang Siantar'),
(2, 'Kepala Kantor Kementerian Agama Kabupaten Deli Serdang', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Medan'),
(2, 'Kepala Kantor Kementerian Agama Kabupaten Labuhan Batu', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Pematang Siantar'),
(2, 'Kepala Kantor Kementerian Agama Kabupaten Labuhan Batu Selatan', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Pematang Siantar'),
(2, 'Kepala Kantor Kementerian Agama Kabupaten Labuhan Batu Utara', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Pematang Siantar'),
(2, 'Kepala Kantor Kementerian Agama Kabupaten Langkat', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Medan'),
(2, 'Kepala Kantor Kementerian Agama Kabupaten Mandailing Natal', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Pematang Siantar'),
(2, 'Kepala Kantor Kementerian Agama Kabupaten Nias', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Kepulauan Nias'),
(2, 'Kepala Kantor Kementerian Agama Kabupaten Nias Barat', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Kepulauan Nias'),
(2, 'Kepala Kantor Kementerian Agama Kabupaten Nias Utara', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Kepulauan Nias'),
(2, 'Kepala Kantor Kementerian Agama Kabupaten Nias Selatan', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Kepulauan Nias'),
(2, 'Kepala Kantor Kementerian Agama Kabupaten Simalungun', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Pematang Siantar'),
(2, 'Kepala Kantor Kementerian Agama Kabupaten Karo', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Medan'),
(2, 'Kepala Kantor Kementerian Agama Kabupaten Tapanuli Selatan', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Pematang Siantar'),
(2, 'Kepala Kantor Kementerian Agama Kabupaten Tapanuli Tengah', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Pematang Siantar'),
(2, 'Kepala Kantor Kementerian Agama Kabupaten Tapanuli Utara', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Pematang Siantar'),
(2, 'Kepala Kantor Kementerian Agama Kabupaten Toba Samosir', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Pematang Siantar'),
(2, 'Kepala Kantor Kementerian Agama Kabupaten Pakpak Barat', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Pematang Siantar'),
(2, 'Kepala Kantor Kementerian Agama Kabupaten Serdang Bedagai', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Medan'),
(2, 'Kepala Kantor Kementerian Agama Kabupaten Samosir', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Pematang Siantar'),
(2, 'Kepala Kantor Kementerian Agama Kabupaten Humbang Hasundutan', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Pematang Siantar'),
(2, 'Kepala Kantor Kementerian Agama Kabupaten Padang Lawas', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Pematang Siantar'),
(2, 'Kepala Kantor Kementerian Agama Kabupaten Padang Lawas Utara', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Pematang Siantar'),
(2, 'Kepala Kantor Kementerian Agama Kabupaten Padang Batu Bara', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Pematang Siantar'),
(2, 'Kepala Kantor Kementerian Agama Kota Gunung Sitoli', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Kepulauan Nias'),
(2, 'Kepala Kantor Kementerian Agama Kota Binjai', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Medan'),
(2, 'Kepala Kantor Kementerian Agama Kota Medan', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Medan'),
(2, 'Kepala Kantor Kementerian Agama Kota Pematang Siantar', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Pematang Siantar'),
(2, 'Kepala Kantor Kementerian Agama Kota Sibolga', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Pematang Siantar'),
(2, 'Kepala Kantor Kementerian Agama Kota Tanjung Balai', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Medan'),
(2, 'Kepala Kantor Kementerian Agama Kota Tebing Tinggi', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Medan'),
(2, 'Kepala Kantor Kementerian Agama Kota Padangsidimpuan', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Pematang Siantar'),
(98, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Serang'),
(97, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Mataram'),
(96, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Palangka Raya'),
(86, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Jayapura'),
(66, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Bukittinggi'),
(46, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Bengkulu'),
(68, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Bengkulu'),
(60, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Palu'),
(75, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Jember'),
(70, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Lampung'),
(76, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Kediri'),
(85, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Kendari'),
(67, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Jambi'),
(71, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Semarang'),
(63, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Banda Aceh'),
(81, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Manado'),
(44, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Pematang Siantar'),
(80, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Palangka Raya'),
(82, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Makassar'),
(77, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Surabaya'),
(83, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Makassar'),
(72, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Pekalongan'),
(78, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Madiun'),
(59, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Pontianak'),
(73, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Purwokerto'),
(74, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Semarang'),
(79, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Samarinda'),
(62, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Bukittinggi'),
(87, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Jayapura'),
(53, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Surakarta'),
(69, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Pangkalpinang'),
(61, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Ternate'),
(54, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Kediri'),
(84, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Makassar'),
(64, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Banda Aceh'),
(89, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Palangka Raya'),
(92, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Jayapura'),
(91, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Makassar'),
(88, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Ambon'),
(90, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Pematang Siantar'),
(93, '', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Manado'),
(135, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Jakarta'),
(42, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Makassar'),
(41, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Malang'),
(36, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Pekanbaru'),
(38, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Bandung'),
(39, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Yogyakarta'),
(37, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Jakarta'),
(143, ' ', 'Kepala Kantor Cabang PT. Tabungan dan Asuransi Pegawai Negeri (Persero) Utama Jakarta'),
(104, 'Badan Litbang dan Diklat Kementerian Agama', 'Jakarta IV'),
(111, 'Balai Diklat Keagamaan Ambon', 'Ambon'),
(105, 'Balai Diklat Keagamaan Bandung', 'Bandung'),
(108, 'Balai Diklat Keagamaan Banjarmasin', 'Banjarmasin'),
(112, 'Balai Diklat Keagamaan Denpasar', 'Denpasar'),
(113, 'Balai Diklat Keagamaan Jakarta', 'Jakarta IV'),
(110, 'Balai Diklat Keagamaan Makassar', 'Makassar'),
(101, 'Balai Diklat Keagamaan Medan', 'Medan'),
(109, 'Balai Diklat Keagamaan Manado', 'Manado'),
(102, 'Balai Diklat Keagamaan Padang', 'Padang'),
(103, 'Balai Diklat Keagamaan Palembang', 'Palembang'),
(106, 'Balai Diklat Keagamaan Semarang', 'Semarang'),
(107, 'Balai Diklat Keagamaan Surabaya', 'Surabaya'),
(113, 'Balai Litbang Agama Jakarta', 'Jakarta IV'),
(115, 'Balai Litbang Agama Makassar', 'Makassar'),
(114, 'Balai Litbang Agama Semarang', 'Semarang'),
(141, 'Direktorat Jenderal Bimbingan Masyarakat Buddha Kementerian Agama', 'Jakarta IV'),
(140, 'Direktorat Jenderal Bimbingan Masyarakat Hindu Kementerian Agama', 'Jakarta IV'),
(137, 'Direktorat Jenderal Bimbingan Masyarakat Islam Kementerian Agama', 'Jakarta IV'),
(139, 'Direktorat Jenderal Bimbingan Masyarakat Katolik Kementerian Agama', 'Jakarta IV'),
(138, 'Direktorat Jenderal Bimbingan Masyarakat Kristen Kementerian Agama', 'Jakarta IV'),
(136, 'Direktorat Jenderal Pendidikan Islam Kementerian Agama', 'Jakarta IV'),
(142, 'Direktorat Jenderal Penyelenggaraan Haji dan Umrah Kementerian Agama', 'Jakarta IV'),
(57, 'Institut Agama Islam Negeri Ambon', 'Ambon'),
(55, 'Institut Agama Islam Negeri Antasari Banjarmasin', 'Banjarmasin'),
(35, 'Universitas Islam Negeri Ar-Raniry Banda Aceh', 'Banda Aceh'),
(45, 'Institut Agama Islam Negeri Imam Bonjol Padang', 'Padang'),
(58, 'Institut Agama Islam Negeri Mataram', 'Mataram'),
(48, 'Institut Agama Islam Negeri Raden Fatah Palembang', 'Palembang'),
(49, 'Institut Agama Islam Negeri Raden Intan Lampung', 'Bandar Lampung'),
(56, 'Institut Agama Islam Negeri Sultan Amai Gorontalo', 'Gorontalo'),
(50, 'Institut Agama Islam Negeri Sultan Maulana Hasanuddin Banten', 'Serang'),
(47, 'Institut Agama Islam Negeri Sulthan Thaha Saifuddin Jambi', 'Jambi'),
(43, 'Institut Agama Islam Negeri Sumatera Utara', 'Medan '),
(40, 'Universitas Islam Negeri Sunan Ampel Surabaya', 'Surabaya '),
(51, 'Institut Agama Islam Negeri Syekh Nurjati Cirebon', 'Cirebon'),
(52, 'Institut Agama Islam Negeri Walisongo Semarang', 'Semarang '),
(95, 'Institut Hindu Dharma Negeri Denpasar', 'Denpasar'),
(1, 'Kantor Wilayah Kementerian Agama Provinsi Aceh', 'Banda Aceh'),
(1, 'Kantor Wilayah Kementerian Agama Provinsi Aceh', 'Meulaboh'),
(1, 'Kantor Wilayah Kementerian Agama Provinsi Aceh', 'Tapaktuan'),
(1, 'Kantor Wilayah Kementerian Agama Provinsi Aceh', 'Takengon'),
(1, 'Kantor Wilayah Kementerian Agama Provinsi Aceh', 'Kutacane'),
(1, 'Kantor Wilayah Kementerian Agama Provinsi Aceh', 'Langsa'),
(1, 'Kantor Wilayah Kementerian Agama Provinsi Aceh', 'Lhokseumawe'),
(28, 'Kantor Wilayah Kementerian Agama Provinsi Bali', 'Denpasar'),
(28, 'Kantor Wilayah Kementerian Agama Provinsi Bali', 'Amlapura'),
(28, 'Kantor Wilayah Kementerian Agama Provinsi Bali', 'Singaraja'),
(11, 'Kantor Wilayah Kementerian Agama Provinsi Banten', 'Serang'),
(11, 'Kantor Wilayah Kementerian Agama Provinsi Banten', 'Rangkasbitung'),
(11, 'Kantor Wilayah Kementerian Agama Provinsi Banten', 'Tangerang'),
(8, 'Kantor Wilayah Kementerian Agama Provinsi Bengkulu', 'Bengkulu'),
(8, 'Kantor Wilayah Kementerian Agama Provinsi Bengkulu', 'Manna'),
(8, 'Kantor Wilayah Kementerian Agama Provinsi Bengkulu', 'Mukomuko'),
(8, 'Kantor Wilayah Kementerian Agama Provinsi Bengkulu', 'Curup'),
(15, 'Kantor Wilayah Kementerian Agama Provinsi Daerah Istimewa Yogyakarta', 'Yogyakarta'),
(15, 'Kantor Wilayah Kementerian Agama Provinsi Daerah Istimewa Yogyakarta', 'Wonosari'),
(15, 'Kantor Wilayah Kementerian Agama Provinsi Daerah Istimewa Yogyakarta', 'Wates'),
(12, 'Kantor Wilayah Kementerian Agama Provinsi DKI Jakarta', 'Jakarta IV'),
(23, 'Kantor Wilayah Kementerian Agama Provinsi Gorontalo', 'Gorontalo'),
(23, 'Kantor Wilayah Kementerian Agama Provinsi Gorontalo', 'Marisa'),
(6, 'Kantor Wilayah Kementerian Agama Provinsi Jambi', 'Jambi'),
(6, 'Kantor Wilayah Kementerian Agama Provinsi Jambi', 'Muara Bungo'),
(6, 'Kantor Wilayah Kementerian Agama Provinsi Jambi', 'Sungai Penuh'),
(6, 'Kantor Wilayah Kementerian Agama Provinsi Jambi', 'Bangko'),
(6, 'Kantor Wilayah Kementerian Agama Provinsi Jambi', 'Kuala Tungkal'),
(13, 'Kantor Wilayah Kementerian Agama Provinsi Jawa Barat', 'Bandung'),
(13, 'Kantor Wilayah Kementerian Agama Provinsi Jawa Barat', 'Bekasi'),
(13, 'Kantor Wilayah Kementerian Agama Provinsi Jawa Barat', 'Bogor'),
(13, 'Kantor Wilayah Kementerian Agama Provinsi Jawa Barat', 'Tasikmalaya'),
(13, 'Kantor Wilayah Kementerian Agama Provinsi Jawa Barat', 'Sukabumi'),
(13, 'Kantor Wilayah Kementerian Agama Provinsi Jawa Barat', 'Cirebon'),
(13, 'Kantor Wilayah Kementerian Agama Provinsi Jawa Barat', 'Garut'),
(13, 'Kantor Wilayah Kementerian Agama Provinsi Jawa Barat', 'Karawang'),
(13, 'Kantor Wilayah Kementerian Agama Provinsi Jawa Barat', 'Kuningan'),
(13, 'Kantor Wilayah Kementerian Agama Provinsi Jawa Barat', 'Purwakarta'),
(13, 'Kantor Wilayah Kementerian Agama Provinsi Jawa Barat', 'Sumedang'),
(14, 'Kantor Wilayah Kementerian Agama Provinsi Jawa Tengah', 'Semarang'),
(14, 'Kantor Wilayah Kementerian Agama Provinsi Jawa Tengah', 'Banjarnegara'),
(14, 'Kantor Wilayah Kementerian Agama Provinsi Jawa Tengah', 'Purwokerto'),
(14, 'Kantor Wilayah Kementerian Agama Provinsi Jawa Tengah', 'Pekalongan'),
(14, 'Kantor Wilayah Kementerian Agama Provinsi Jawa Tengah', 'Purwodadi'),
(14, 'Kantor Wilayah Kementerian Agama Provinsi Jawa Tengah', 'Klaten'),
(14, 'Kantor Wilayah Kementerian Agama Provinsi Jawa Tengah', 'Tegal'),
(14, 'Kantor Wilayah Kementerian Agama Provinsi Jawa Tengah', 'Cilacap'),
(14, 'Kantor Wilayah Kementerian Agama Provinsi Jawa Tengah', 'Kudus'),
(14, 'Kantor Wilayah Kementerian Agama Provinsi Jawa Tengah', 'Purworejo'),
(14, 'Kantor Wilayah Kementerian Agama Provinsi Jawa Tengah', 'Magelang'),
(14, 'Kantor Wilayah Kementerian Agama Provinsi Jawa Tengah', 'Pati'),
(14, 'Kantor Wilayah Kementerian Agama Provinsi Jawa Tengah', 'Sragen'),
(14, 'Kantor Wilayah Kementerian Agama Provinsi Jawa Tengah', 'Surakarta'),
(16, 'Kantor Wilayah Kementerian Agama Provinsi Jawa Timur', 'Surabaya'),
(16, 'Kantor Wilayah Kementerian Agama Provinsi Jawa Timur', 'Pamekasan'),
(16, 'Kantor Wilayah Kementerian Agama Provinsi Jawa Timur', 'Banyuwangi'),
(16, 'Kantor Wilayah Kementerian Agama Provinsi Jawa Timur', 'Blitar'),
(16, 'Kantor Wilayah Kementerian Agama Provinsi Jawa Timur', 'Bojonegoro'),
(16, 'Kantor Wilayah Kementerian Agama Provinsi Jawa Timur', 'Bondowoso'),
(16, 'Kantor Wilayah Kementerian Agama Provinsi Jawa Timur', 'Jember'),
(16, 'Kantor Wilayah Kementerian Agama Provinsi Jawa Timur', 'Mojokerto'),
(16, 'Kantor Wilayah Kementerian Agama Provinsi Jawa Timur', 'Kediri'),
(16, 'Kantor Wilayah Kementerian Agama Provinsi Jawa Timur', 'Madiun'),
(16, 'Kantor Wilayah Kementerian Agama Provinsi Jawa Timur', 'Malang'),
(16, 'Kantor Wilayah Kementerian Agama Provinsi Jawa Timur', 'Pacitan'),
(16, 'Kantor Wilayah Kementerian Agama Provinsi Jawa Timur', 'Sidoarjo'),
(16, 'Kantor Wilayah Kementerian Agama Provinsi Jawa Timur', 'Tuban'),
(17, 'Kantor Wilayah Kementerian Agama Provinsi Kalimantan Barat', 'Pontianak'),
(17, 'Kantor Wilayah Kementerian Agama Provinsi Kalimantan Barat', 'Singkawang'),
(17, 'Kantor Wilayah Kementerian Agama Provinsi Kalimantan Barat', 'Putussibau'),
(17, 'Kantor Wilayah Kementerian Agama Provinsi Kalimantan Barat', 'Ketapang'),
(17, 'Kantor Wilayah Kementerian Agama Provinsi Kalimantan Barat', 'Sanggau'),
(17, 'Kantor Wilayah Kementerian Agama Provinsi Kalimantan Barat', 'Sintang'),
(19, 'Kantor Wilayah Kementerian Agama Provinsi Kalimantan Selatan', 'Banjarmasin'),
(19, 'Kantor Wilayah Kementerian Agama Provinsi Kalimantan Selatan', 'Barabai'),
(19, 'Kantor Wilayah Kementerian Agama Provinsi Kalimantan Selatan', 'Tanjung'),
(19, 'Kantor Wilayah Kementerian Agama Provinsi Kalimantan Selatan', 'Kotabaru'),
(19, 'Kantor Wilayah Kementerian Agama Provinsi Kalimantan Selatan', 'Pelaihari'),
(18, 'Kantor Wilayah Kementerian Agama Provinsi Kalimantan Tengah', 'Palangka Raya'),
(18, 'Kantor Wilayah Kementerian Agama Provinsi Kalimantan Tengah', 'Buntok'),
(18, 'Kantor Wilayah Kementerian Agama Provinsi Kalimantan Tengah', 'Pangkalan Bun'),
(18, 'Kantor Wilayah Kementerian Agama Provinsi Kalimantan Tengah', 'Sampit'),
(20, 'Kantor Wilayah Kementerian Agama Provinsi Kalimantan Timur', 'Samarinda'),
(20, 'Kantor Wilayah Kementerian Agama Provinsi Kalimantan Timur', 'Tanjungredep'),
(20, 'Kantor Wilayah Kementerian Agama Provinsi Kalimantan Timur', 'Tarakan'),
(20, 'Kantor Wilayah Kementerian Agama Provinsi Kalimantan Timur', 'Nunukan'),
(20, 'Kantor Wilayah Kementerian Agama Provinsi Kalimantan Timur', 'Balikpapan'),
(10, 'Kantor Wilayah Kementerian Agama Provinsi Kepulauan Bangka Belitung', 'Pangkalpinang'),
(10, 'Kantor Wilayah Kementerian Agama Provinsi Kepulauan Bangka Belitung', 'Tanjungpandan'),
(5, 'Kantor Wilayah Kementerian Agama Provinsi Kepulauan Riau', 'Tanjungpinang'),
(5, 'Kantor Wilayah Kementerian Agama Provinsi Kepulauan Riau', 'Batam'),
(9, 'Kantor Wilayah Kementerian Agama Provinsi Lampung', 'Bandar Lampung'),
(9, 'Kantor Wilayah Kementerian Agama Provinsi Lampung', 'Liwa'),
(9, 'Kantor Wilayah Kementerian Agama Provinsi Lampung', 'Metro'),
(9, 'Kantor Wilayah Kementerian Agama Provinsi Lampung', 'Kota Bumi'),
(31, 'Kantor Wilayah Kementerian Agama Provinsi Maluku', 'Ambon'),
(31, 'Kantor Wilayah Kementerian Agama Provinsi Maluku', 'Masohi'),
(31, 'Kantor Wilayah Kementerian Agama Provinsi Maluku', 'Tual'),
(31, 'Kantor Wilayah Kementerian Agama Provinsi Maluku', 'Saumlaki'),
(32, 'Kantor Wilayah Kementerian Agama Provinsi Maluku Utara', 'Ternate'),
(32, 'Kantor Wilayah Kementerian Agama Provinsi Maluku Utara', 'Tobelo'),
(29, 'Kantor Wilayah Kementerian Agama Provinsi Nusa Tenggara Barat', 'Mataram'),
(29, 'Kantor Wilayah Kementerian Agama Provinsi Nusa Tenggara Barat', 'Bima'),
(29, 'Kantor Wilayah Kementerian Agama Provinsi Nusa Tenggara Barat', 'Selong'),
(29, 'Kantor Wilayah Kementerian Agama Provinsi Nusa Tenggara Barat', 'Sumbawa Besar'),
(30, 'Kantor Wilayah Kementerian Agama Provinsi Nusa Tenggara Timur', 'Kupang'),
(30, 'Kantor Wilayah Kementerian Agama Provinsi Nusa Tenggara Timur', 'Atambua'),
(30, 'Kantor Wilayah Kementerian Agama Provinsi Nusa Tenggara Timur', 'Ende'),
(30, 'Kantor Wilayah Kementerian Agama Provinsi Nusa Tenggara Timur', 'Larantuka'),
(30, 'Kantor Wilayah Kementerian Agama Provinsi Nusa Tenggara Timur', 'Ruteng'),
(30, 'Kantor Wilayah Kementerian Agama Provinsi Nusa Tenggara Timur', 'Waingapu'),
(33, 'Kantor Wilayah Kementerian Agama Provinsi Papua', 'Jayapura'),
(33, 'Kantor Wilayah Kementerian Agama Provinsi Papua', 'Wamena'),
(33, 'Kantor Wilayah Kementerian Agama Provinsi Papua', 'Merauke'),
(33, 'Kantor Wilayah Kementerian Agama Provinsi Papua', 'Timika'),
(33, 'Kantor Wilayah Kementerian Agama Provinsi Papua', 'Nabire'),
(33, 'Kantor Wilayah Kementerian Agama Provinsi Papua', 'Biak'),
(33, 'Kantor Wilayah Kementerian Agama Provinsi Papua', 'Serui'),
(34, 'Kantor Wilayah Kementerian Agama Provinsi Papua Barat', 'Manokwari'),
(34, 'Kantor Wilayah Kementerian Agama Provinsi Papua Barat', 'Sorong'),
(34, 'Kantor Wilayah Kementerian Agama Provinsi Papua Barat', 'Fak-Fak'),
(4, 'Kantor Wilayah Kementerian Agama Provinsi Riau', 'Pekanbaru'),
(4, 'Kantor Wilayah Kementerian Agama Provinsi Riau', 'Dumai'),
(4, 'Kantor Wilayah Kementerian Agama Provinsi Riau', 'Rengat'),
(26, 'Kantor Wilayah Kementerian Agama Provinsi Sulawesi Barat', 'Mamuju'),
(26, 'Kantor Wilayah Kementerian Agama Provinsi Sulawesi Barat', 'Majene'),
(27, 'Kantor Wilayah Kementerian Agama Provinsi Sulawesi Selatan', 'Makassar'),
(27, 'Kantor Wilayah Kementerian Agama Provinsi Sulawesi Selatan', 'Bantaeng'),
(27, 'Kantor Wilayah Kementerian Agama Provinsi Sulawesi Selatan', 'Parepare'),
(27, 'Kantor Wilayah Kementerian Agama Provinsi Sulawesi Selatan', 'Watampone'),
(27, 'Kantor Wilayah Kementerian Agama Provinsi Sulawesi Selatan', 'Palopo'),
(27, 'Kantor Wilayah Kementerian Agama Provinsi Sulawesi Selatan', 'Benteng'),
(27, 'Kantor Wilayah Kementerian Agama Provinsi Sulawesi Selatan', 'Sinjai'),
(27, 'Kantor Wilayah Kementerian Agama Provinsi Sulawesi Selatan', 'Makale'),
(24, 'Kantor Wilayah Kementerian Agama Provinsi Sulawesi Tengah', 'Palu'),
(24, 'Kantor Wilayah Kementerian Agama Provinsi Sulawesi Tengah', 'Luwuk'),
(24, 'Kantor Wilayah Kementerian Agama Provinsi Sulawesi Tengah', 'Toli-toli'),
(24, 'Kantor Wilayah Kementerian Agama Provinsi Sulawesi Tengah', 'Poso'),
(25, 'Kantor Wilayah Kementerian Agama Provinsi Sulawesi Tenggara', 'Kendari'),
(25, 'Kantor Wilayah Kementerian Agama Provinsi Sulawesi Tenggara', 'Kolaka'),
(25, 'Kantor Wilayah Kementerian Agama Provinsi Sulawesi Tenggara', 'Raha'),
(25, 'Kantor Wilayah Kementerian Agama Provinsi Sulawesi Tenggara', 'Bau-Bau'),
(22, 'Kantor Wilayah Kementerian Agama Provinsi Sulawesi Utara', 'Manado'),
(22, 'Kantor Wilayah Kementerian Agama Provinsi Sulawesi Utara', 'Kotamobagu'),
(22, 'Kantor Wilayah Kementerian Agama Provinsi Sulawesi Utara', 'Bitung'),
(22, 'Kantor Wilayah Kementerian Agama Provinsi Sulawesi Utara', 'Tahuna'),
(3, 'Kantor Wilayah Kementerian Agama Provinsi Sumatera Barat', 'Padang'),
(3, 'Kantor Wilayah Kementerian Agama Provinsi Sumatera Barat', 'Bukittinggi'),
(3, 'Kantor Wilayah Kementerian Agama Provinsi Sumatera Barat', 'Lubuk Sikaping'),
(3, 'Kantor Wilayah Kementerian Agama Provinsi Sumatera Barat', 'Painan'),
(3, 'Kantor Wilayah Kementerian Agama Provinsi Sumatera Barat', 'Sijunjung'),
(3, 'Kantor Wilayah Kementerian Agama Provinsi Sumatera Barat', 'Solok'),
(7, 'Kantor Wilayah Kementerian Agama Provinsi Sumatera Selatan', 'Palembang'),
(7, 'Kantor Wilayah Kementerian Agama Provinsi Sumatera Selatan', 'Lahat'),
(7, 'Kantor Wilayah Kementerian Agama Provinsi Sumatera Selatan', 'Sekayu'),
(7, 'Kantor Wilayah Kementerian Agama Provinsi Sumatera Selatan', 'Lubuk Linggau'),
(7, 'Kantor Wilayah Kementerian Agama Provinsi Sumatera Selatan', 'Baturaja'),
(7, 'Kantor Wilayah Kementerian Agama Provinsi Sumatera Selatan', ''),
(2, 'Kantor Wilayah Kementerian Agama Provinsi Sumatera Utara', 'Medan'),
(2, 'Kantor Wilayah Kementerian Agama Provinsi Sumatera Utara', 'Tanjung Balai'),
(2, 'Kantor Wilayah Kementerian Agama Provinsi Sumatera Utara', 'Sidikalang'),
(2, 'Kantor Wilayah Kementerian Agama Provinsi Sumatera Utara', 'Tebing Tinggi'),
(2, 'Kantor Wilayah Kementerian Agama Provinsi Sumatera Utara', 'Rantau Prapat'),
(2, 'Kantor Wilayah Kementerian Agama Provinsi Sumatera Utara', 'Padangsidimpuan'),
(2, 'Kantor Wilayah Kementerian Agama Provinsi Sumatera Utara', 'Gunung Sitoli'),
(2, 'Kantor Wilayah Kementerian Agama Provinsi Sumatera Utara', 'Pematang Siantar'),
(2, 'Kantor Wilayah Kementerian Agama Provinsi Sumatera Utara', 'Sibolga'),
(2, 'Kantor Wilayah Kementerian Agama Provinsi Sumatera Utara', 'Balige'),
(98, 'Sekolah Tinggi Agama Buddha Negeri Sriwijaya Tangerang', 'Tangerang'),
(97, 'Sekolah Tinggi Agama Hindu Negeri Gde Pudja Mataram', 'Mataram'),
(96, 'Sekolah Tinggi Agama Hindu Negeri Tampung Penyang Palangka Raya', 'Palangka Raya'),
(86, 'Sekolah Tinggi Agama Islam Negeri Al Fatah Jayapura', 'Jayapura'),
(66, 'Sekolah Tinggi Agama Islam Negeri Batusangkar', 'Bukittinggi'),
(46, 'Institut Agama Islam Negeri Bengkulu', 'Bengkulu'),
(68, 'Sekolah Tinggi Agama Islam Negeri Curup', 'Curup'),
(60, 'Institut Agama Islam Negeri Datokarama Palu', 'Palu'),
(75, 'Sekolah Tinggi Agama Islam Negeri Jember', 'Jember'),
(70, 'Sekolah Tinggi Agama Islam Negeri Jurai Siwo Metro', 'Metro'),
(76, 'Sekolah Tinggi Agama Islam Negeri Kediri', 'Kediri'),
(85, 'Sekolah Tinggi Agama Islam Negeri Sultan Qaimuddin Kendari', 'Kendari'),
(67, 'Sekolah Tinggi Agama Islam Negeri Kerinci', 'Sungai Penuh'),
(71, 'Sekolah Tinggi Agama Islam Negeri Kudus', 'Kudus'),
(63, 'Sekolah Tinggi Agama Islam Negeri Malikussaleh Lhokseumawe', 'Lhokseumawe'),
(81, 'Sekolah Tinggi Agama Islam Negeri Manado', 'Manado'),
(44, 'Institut Agama Islam Negeri Padangsidimpuan', 'Padangsidimpuan');
INSERT INTO `tbl_taspen` (`KODE_SATUAN_KERJA`, `Tembusan`, `Taspen`) VALUES
(80, 'Sekolah Tinggi Agama Islam Negeri Palangka Raya', 'Palangka Raya'),
(82, 'Sekolah Tinggi Agama Islam Negeri Palopo', 'Palopo'),
(77, 'Sekolah Tinggi Agama Islam Negeri Pamekasan', 'Pamekasan'),
(83, 'Sekolah Tinggi Agama Islam Negeri Parepare', 'Parepare'),
(72, 'Sekolah Tinggi Agama Islam Negeri Pekalongan', 'Pekalongan'),
(78, 'Sekolah Tinggi Agama Islam Negeri Ponorogo', 'Madiun'),
(59, 'Sekolah Tinggi Agama Islam Negeri Pontianak', 'Pontianak'),
(73, 'Sekolah Tinggi Agama Islam Negeri Purwokerto', 'Purwokerto'),
(74, 'Sekolah Tinggi Agama Islam Negeri Salatiga', 'Semarang '),
(79, 'Sekolah Tinggi Agama Islam Negeri Samarinda', 'Samarinda'),
(62, 'Sekolah Tinggi Agama Islam Negeri Sjech M. Djamil Djambek Bukittinggi', 'Bukittinggi'),
(87, 'Sekolah Tinggi Agama Islam Negeri Sorong', 'Sorong'),
(53, 'Institut Agama Islam Negeri Surakarta', 'Surakarta'),
(69, 'Sekolah Tinggi Agama Islam Negeri Syaikh Abdurrahman Siddik Bangka Belitung', 'Pangkalpinang'),
(61, 'Sekolah Tinggi Agama Islam Negeri Ternate', 'Ternate'),
(54, 'Institut Agama Islam Negeri Tulungagung', 'Blitar'),
(84, 'Sekolah Tinggi Agama Islam Negeri Watampone', 'Watampone'),
(64, 'Sekolah Tinggi Agama Islam Negeri Zawiyah Cot Kala Langsa', 'Langsa'),
(89, 'Sekolah Tinggi Agama Kristen Negeri Palangka Raya', 'Palangka Raya'),
(92, 'Sekolah Tinggi Agama Kristen Negeri Sentani', 'Jayapura'),
(91, 'Sekolah Tinggi Agama Kristen Negeri Toraja', 'Makale'),
(88, 'Sekolah Tinggi Agama Kristen Negeri Ambon', 'Ambon'),
(90, 'Sekolah Tinggi Agama Kristen Negeri Tarutung', 'Balige'),
(93, 'Sekolah Tinggi Agama Kristen Negeri Manado', 'Manado'),
(135, 'Sekretariat Jenderal Kementerian Agama', 'Jakarta IV'),
(42, 'Universitas Islam Negeri Alauddin Makassar', 'Makasar '),
(41, 'Universitas Islam Negeri Maulana Malik Ibrahim Malang', 'Malang'),
(36, 'Universitas Islam Negeri Sultan Syarif Kasim Riau', 'Pekanbaru'),
(38, 'Universitas Islam Negeri Sunan Gunung Djati Bandung', 'Bandung '),
(39, 'Universitas Islam Negeri Sunan Kalijaga Yogyakarta', 'Yogyakarta'),
(37, 'Universitas Islam Negeri Syarif Hidayatullah Jakarta', 'Jakarta IV'),
(143, 'Inspektorat Jenderal', 'Jakarta IV');

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
  `Created_User` varchar(100) NOT NULL,
  `Created_Date` datetime NOT NULL,
  `Updated_User` varchar(100) NOT NULL,
  `Updated_Date` datetime NOT NULL,
  `FileTelaah` varchar(255) NOT NULL,
  `Proses` int(1) NOT NULL,
  `TelaahNo` varchar(30) DEFAULT NULL,
  `Tanggal_Telaah` date DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `tbl_telaah`
--

INSERT INTO `tbl_telaah` (`ID`, `NIP`, `Fullname`, `Dasar_dan_Bukti_Penunjang`, `Pelanggaran`, `Pasal_Pelanggaran`, `Rekomendasi_Hukuman`, `Analisa_dan_Pertimbangan`, `Keputusan_Sidang_DPK`, `Created_User`, `Created_Date`, `Updated_User`, `Updated_Date`, `FileTelaah`, `Proses`, `TelaahNo`, `Tanggal_Telaah`) VALUES
(21, '197002052000031001', 'DUDANG GOJALI, M.Ag.', 'Surat Komisi Aparatur Sipil Negara  Nomor B-3590/KASN/10/2019 tanggal 28 Oktober 2019 perihal Rekomendasi terkait Dugaan Pelanggaran dalam Pengangkatan Ketua Panitia Penjaringan, Wakil Rektor, Dekan Fakultas, dan Dugaan Pelanggaran Kode Etik/Kode serta Rangkap Jabatan oleh Aparatur Negara (ASN) di lingkungan Universitas Islam Negeri Sunan Gunung Djati Bandung dan; \nBerita Acara Permintaan Keterangan oleh Tim Komisi Aparatur Sipil Negara tanggal 4 Oktober 2019 terhadap Sdr. Dudang Gojali, M.Ag NIP 197002052000031001.', 'Sdr Dudang Gojali, M.Ag mengakui berpacaran selama setahun dengan Sdr. Rahayu Kusumadewi Lektor jurusan Administrasi Negara Fakultas Ilmu Sosial dan Ilmu Politik UIN Sunan Gunung Djati Bandung lalu menikah pada Agustus 2013 yang tidak prosedural yaitu menikah secara agama dan dilangsungkan tertutup serta saksi dari pihak perempuan hanya orang tua dari pihak perempuan.\nPernikahan tidak prosedural yang bersangkutan dengan Sdr. Rahayu telah memiliki 1 anak perempuan serta yang bersangkutan menyatakan tidak dapat berpisah karena sudah menyayangi.\nPada bulan Oktober 2017 yang bersangkutan mengakui telah menalak 3 (tiga) Sdr. Rahayu karena merasa bahwa poligami tidak nyaman lalu memutuskan sepakat untuk bercerai tetapi masih berkunjung hingga saat ini untuk menemui anak, dan sdr Rahayu sampai saat ini belum menikah lagi dan yang bersangkutan menyadari telah melanggar ketentuan tentang Peraturan Pemerintah Nomor 45 Tahun 1990 tentang Izin Perkawinan dan Perceraian  Bagi Pegawai Negeri Sipil.\n', 'Yang bersangkutan melanggar ketentuan Pasal 4 ayat (1) Peraturan Pemerintah Nomor 45 Tahun 1990 tentang Izin Perkawinan dan Perceraian  Bagi Pegawai Negeri Sipil dan Pasal 3 angka 4 dan angka 6 Peraturan Pemerintah Nomor 53 Tahun 2010 tentang Disiplin Pegawai Negeri Sipil.', 'Yang bersangkutan direkomendasikan oleh Komisi Aparatur Sipil Negara untuk dijatuhi hukuman disiplin tingkat berat.', 'Berdasarkan surat Kepala Biro Kepegawaian Nomor R-36803/B-II/2-b/Kp.04.1/12/2020 tanggal 11 Desember 2019 telah memohon kepada Rektor Universitas Negeri (UIN) Sunan Gunung Djati Bandung tentang kelengkapan data pendukung terhadap Sdr. Dudang Gojali, M.Ag berupa surat pemanggilan, Berita Acara Pemeriksaan serta hukuman disiplin dari atasan selama yang bersangkutan melakukan pernikahan tidak prosedural.', '', 'ALAMSYAH, S.E', '2022-05-27 13:49:54', 'ALAMSYAH, S.E', '0000-00-00 00:00:00', 'Telaah_197002052000031001_27052022_093528_27052022_014949.pdf', 1, 'R-001/B.II/2-b/KP.04.1/05/2022', '2022-05-24'),
(23, '197508082001121001', 'MUHAMMAD IKBAL, S.Ag.', 'Surat Kepala Kantor Wilayah kementerian Agama Provinsi Jawa Barat R-10251/Kw.10/KP.04.2/11/2021 tanggal 17 November 2021 tentang Penjatuhan Hukuman Disiplin Pegawai Negeri Sipil a.n. Muhammad Ikbal, S.Ag; Berita Acara Pemeriksaan tanggal  10 Oktober 2019.', 'Tidak masuk kerja', 'Pasal 4 huruf f PP 94 Tahun 2021', 'Sesuai dengan Peraturan Perundang-undangan', '-', '', 'ALAMSYAH, S.E', '2022-05-30 13:25:16', 'ALAMSYAH, S.E', '0000-00-00 00:00:00', 'SURAT PENGANTAR HASIL HUKDIS 2019-2022_30052022_012512.pdf', 0, 'R-003/B.II/2-b/KP.04.1/05/2022', '2022-05-31'),
(24, '196906172003121001', 'DRS. ADAM', 'Surat Kepala Kantor Kementerian Agama Provinsi DKI Jakarta Agama Nomor R-1911/Kw/09.1/3/Kp04.1/02/2022 tanggal 18 Februari 2022 perihal Permohonan Penjatuhan Hukuman Disiplin PNS atas nama Drs. Adam; dan\nBerita Acara Pemeriksaan (BAP) tanggal 29 Desember 2021 terhadap Sdr. Drs. Adam NIP. 196906172003121001', 'Sdr. Drs. Adam diduga melakukan tindak pidana kekerasan dalam rumah tangga terhadap anak kandung dan istri sahnya (terlampir bukti surat pemanggilan pihak kepolisian resor metro depok dan masalah diselesaikan secara kekeluargaan) ;\nYang bersangkutan melakukan perselingkuhan dengan beberapa wanita sejak diangkat sebagai Penghulu pada KUA Jagakarsa tahun 2005-2006, bukti-bukti berupa foto dan chat dengan salah satu wanita selingkuhannya Sdr. Nurhasanah.  Istri sahnya pada tahun 2008, tahun 2010 dan tahun 2013 mengajukan gugatan cerai namun yang bersangkutan menolak dan dipersulit; dan\nYang bersangkutan berdasarkan bukti chat whatsapp terlibat jaringan Tahaja Kartono sebagai calo CPNS', 'Pasal 3 huruf f Pemerintah Nomor 94 Tahun 2021 tentang Disiplin Pegawai Negeri Sipil.', 'Yang bersangkutan direkomendasikan oleh Kantor Wilayah Kementerian Agama Provinsi DKI Jakarta untuk dijatuhi hukuman disiplin sesuai Peraturan Perundang-undangan yang belaku.', 'Yang bersangkutan pernah dijatuhi hukuman disiplin berupa Penundaan Kenaikan Pangkat Selama 1 (satu) Tahun dengan Surat Keputusan Menteri Agama Republik Indonesia Nomor B.II/3/PKP.1/36026 tanggal 3 Desember 2018.', '', 'WIDI TANURJAYA, S.H', '2022-06-02 07:58:06', 'WIDI TANURJAYA, S.H', '2022-06-01 18:44:08', 'No files selected.', 1, NULL, '2022-05-19');

-- --------------------------------------------------------

--
-- Table structure for table `tbl_tembusan`
--

CREATE TABLE `tbl_tembusan` (
  `KODE_SATUAN_KERJA` int(11) NOT NULL,
  `Tembusan` varchar(255) NOT NULL,
  `Tipe` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `tbl_tembusan`
--

INSERT INTO `tbl_tembusan` (`KODE_SATUAN_KERJA`, `Tembusan`, `Tipe`) VALUES
(1, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(1, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(1, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(1, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(1, 'Kantor Wilayah Kementerian Agama Provinsi Aceh', 6),
(1, 'Kepala Kantor Kementerian Agama Kabupaten Aceh Barat', 9),
(1, 'Kepala Kantor Kementerian Agama Kabupaten Aceh Barat Daya', 9),
(1, 'Kepala Kantor Kementerian Agama Kabupaten Aceh Besar', 9),
(1, 'Kepala Kantor Kementerian Agama Kabupaten Aceh Jaya', 9),
(1, 'Kepala Kantor Kementerian Agama Kabupaten Aceh Selatan', 9),
(1, 'Kepala Kantor Kementerian Agama Kabupaten Aceh Singkil', 9),
(1, 'Kepala Kantor Kementerian Agama Kabupaten Aceh Tamiang', 9),
(1, 'Kepala Kantor Kementerian Agama Kabupaten Aceh Tengah', 9),
(1, 'Kepala Kantor Kementerian Agama Kabupaten Aceh Tenggara', 9),
(1, 'Kepala Kantor Kementerian Agama Kabupaten Aceh Timur', 9),
(1, 'Kepala Kantor Kementerian Agama Kabupaten Aceh Utara', 9),
(1, 'Kepala Kantor Kementerian Agama Kabupaten Bener Meriah', 9),
(1, 'Kepala Kantor Kementerian Agama Kabupaten Bireuen', 9),
(1, 'Kepala Kantor Kementerian Agama Kabupaten Gayo Lues', 9),
(1, 'Kepala Kantor Kementerian Agama Kabupaten Nagan Raya', 9),
(1, 'Kepala Kantor Kementerian Agama Kabupaten Pidie', 9),
(1, 'Kepala Kantor Kementerian Agama Kabupaten Pidie Jaya', 9),
(1, 'Kepala Kantor Kementerian Agama Kabupaten Simeuleue', 9),
(1, 'Kepala Kantor Kementerian Agama Kota Banda Aceh', 9),
(1, 'Kepala Kantor Kementerian Agama Kota Langsa', 9),
(1, 'Kepala Kantor Kementerian Agama Kota Lhokseumawe', 9),
(1, 'Kepala Kantor Kementerian Agama Kota Sabang', 9),
(1, 'Kepala Kantor Kementerian Agama Kota Subulussalam', 9),
(1, 'Kepala Kantor Regional VI BKN Medan', 8),
(1, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(1, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(2, ' ', 9),
(2, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(2, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(2, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(2, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(2, 'Kantor Wilayah Kementerian Agama Provinsi Sumatera Utara', 6),
(2, 'Kepala Kantor Kementerian Agama Kabupaten Asahan', 9),
(2, 'Kepala Kantor Kementerian Agama Kabupaten Dairi', 9),
(2, 'Kepala Kantor Kementerian Agama Kabupaten Deli Serdang', 9),
(2, 'Kepala Kantor Kementerian Agama Kabupaten Humbang Hasundutan', 9),
(2, 'Kepala Kantor Kementerian Agama Kabupaten Karo', 9),
(2, 'Kepala Kantor Kementerian Agama Kabupaten Labuhan Batu', 9),
(2, 'Kepala Kantor Kementerian Agama Kabupaten Labuhan Batu Selatan', 9),
(2, 'Kepala Kantor Kementerian Agama Kabupaten Labuhan Batu Utara', 9),
(2, 'Kepala Kantor Kementerian Agama Kabupaten Langkat', 9),
(2, 'Kepala Kantor Kementerian Agama Kabupaten Mandailing Natal', 9),
(2, 'Kepala Kantor Kementerian Agama Kabupaten Nias', 9),
(2, 'Kepala Kantor Kementerian Agama Kabupaten Nias Barat', 9),
(2, 'Kepala Kantor Kementerian Agama Kabupaten Nias Selatan', 9),
(2, 'Kepala Kantor Kementerian Agama Kabupaten Nias Utara', 9),
(2, 'Kepala Kantor Kementerian Agama Kabupaten Padang Batu Bara', 9),
(2, 'Kepala Kantor Kementerian Agama Kabupaten Padang Lawas', 9),
(2, 'Kepala Kantor Kementerian Agama Kabupaten Padang Lawas Utara', 9),
(2, 'Kepala Kantor Kementerian Agama Kabupaten Pakpak Barat', 9),
(2, 'Kepala Kantor Kementerian Agama Kabupaten Samosir', 9),
(2, 'Kepala Kantor Kementerian Agama Kabupaten Serdang Bedagai', 9),
(2, 'Kepala Kantor Kementerian Agama Kabupaten Simalungun', 9),
(2, 'Kepala Kantor Kementerian Agama Kabupaten Tapanuli Selatan', 9),
(2, 'Kepala Kantor Kementerian Agama Kabupaten Tapanuli Tengah', 9),
(2, 'Kepala Kantor Kementerian Agama Kabupaten Tapanuli Utara', 9),
(2, 'Kepala Kantor Kementerian Agama Kabupaten Toba Samosir', 9),
(2, 'Kepala Kantor Kementerian Agama Kota Binjai', 9),
(2, 'Kepala Kantor Kementerian Agama Kota Gunung Sitoli', 9),
(2, 'Kepala Kantor Kementerian Agama Kota Medan', 9),
(2, 'Kepala Kantor Kementerian Agama Kota Padangsidimpuan', 9),
(2, 'Kepala Kantor Kementerian Agama Kota Pematang Siantar', 9),
(2, 'Kepala Kantor Kementerian Agama Kota Sibolga', 9),
(2, 'Kepala Kantor Kementerian Agama Kota Tanjung Balai', 9),
(2, 'Kepala Kantor Kementerian Agama Kota Tebing Tinggi', 9),
(2, 'Kepala Kantor Regional VI BKN Medan', 8),
(2, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(2, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(3, ' ', 9),
(3, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(3, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(3, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(3, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(3, 'Kantor Wilayah Kementerian Agama Provinsi Sumatera Barat', 6),
(3, 'Kepala Kantor Kementerian Agama Kabupaten Agam', 9),
(3, 'Kepala Kantor Kementerian Agama Kabupaten Dharmasraya', 9),
(3, 'Kepala Kantor Kementerian Agama Kabupaten Kepulauan Mentawai', 9),
(3, 'Kepala Kantor Kementerian Agama Kabupaten Limapuluh Kota', 9),
(3, 'Kepala Kantor Kementerian Agama Kabupaten Padang Pariaman', 9),
(3, 'Kepala Kantor Kementerian Agama Kabupaten Pasaman', 9),
(3, 'Kepala Kantor Kementerian Agama Kabupaten Pasaman Barat', 9),
(3, 'Kepala Kantor Kementerian Agama Kabupaten Pesisir Selatan', 9),
(3, 'Kepala Kantor Kementerian Agama Kabupaten Sawahlunto/Sijunjung', 9),
(3, 'Kepala Kantor Kementerian Agama Kabupaten Solok', 9),
(3, 'Kepala Kantor Kementerian Agama Kabupaten Solok Selatan', 9),
(3, 'Kepala Kantor Kementerian Agama Kabupaten Tanah Datar', 9),
(3, 'Kepala Kantor Kementerian Agama Kota Bukittinggi', 9),
(3, 'Kepala Kantor Kementerian Agama Kota Padang', 9),
(3, 'Kepala Kantor Kementerian Agama Kota Padang Panjang', 9),
(3, 'Kepala Kantor Kementerian Agama Kota Pariaman', 9),
(3, 'Kepala Kantor Kementerian Agama Kota Payakumbuh', 9),
(3, 'Kepala Kantor Kementerian Agama Kota Sawahlunto', 9),
(3, 'Kepala Kantor Kementerian Agama Kota Solok', 9),
(3, 'Kepala Kantor Regional XII BKN Pekanbaru', 8),
(3, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(3, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(4, ' ', 9),
(4, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(4, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(4, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(4, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(4, 'Kantor Wilayah Kementerian Agama Provinsi Riau', 6),
(4, 'Kepala Kantor Kementerian Agama Kabupaten Bengkalis', 9),
(4, 'Kepala Kantor Kementerian Agama Kabupaten Indragiri Hilir', 9),
(4, 'Kepala Kantor Kementerian Agama Kabupaten Indragiri Hulu', 9),
(4, 'Kepala Kantor Kementerian Agama Kabupaten Kampar', 9),
(4, 'Kepala Kantor Kementerian Agama Kabupaten Kuantan Sengingi', 9),
(4, 'Kepala Kantor Kementerian Agama Kabupaten Pelalawan', 9),
(4, 'Kepala Kantor Kementerian Agama Kabupaten Rokan Hilir', 9),
(4, 'Kepala Kantor Kementerian Agama Kabupaten Rokan Hulu', 9),
(4, 'Kepala Kantor Kementerian Agama Kabupaten Siak', 9),
(4, 'Kepala Kantor Kementerian Agama Kepulauan Meranti', 9),
(4, 'Kepala Kantor Kementerian Agama Kota Dumai', 9),
(4, 'Kepala Kantor Kementerian Agama Kota Pekanbaru', 9),
(4, 'Kepala Kantor Regional XII BKN Pekanbaru', 8),
(4, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(4, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(5, ' ', 9),
(5, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(5, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(5, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(5, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(5, 'Kantor Wilayah Kementerian Agama Provinsi Kepulauan Riau', 6),
(5, 'Kepala Kantor Kementerian Agama Kabupaten Anambas', 9),
(5, 'Kepala Kantor Kementerian Agama Kabupaten Bintan', 9),
(5, 'Kepala Kantor Kementerian Agama Kabupaten Karimun', 9),
(5, 'Kepala Kantor Kementerian Agama Kabupaten Lingga', 9),
(5, 'Kepala Kantor Kementerian Agama Kabupaten Natuna', 9),
(5, 'Kepala Kantor Kementerian Agama Kota Batam', 9),
(5, 'Kepala Kantor Kementerian Agama Kota Tanjungpinang', 9),
(5, 'Kepala Kantor Regional XII BKN Pekanbaru', 8),
(5, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(5, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(6, ' ', 9),
(6, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(6, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(6, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(6, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(6, 'Kantor Wilayah Kementerian Agama Provinsi Jambi', 6),
(6, 'Kepala Kantor Kementerian Agama Kabupaten Batanghari', 9),
(6, 'Kepala Kantor Kementerian Agama Kabupaten Bungo', 9),
(6, 'Kepala Kantor Kementerian Agama Kabupaten Kerinci', 9),
(6, 'Kepala Kantor Kementerian Agama Kabupaten Merangin', 9),
(6, 'Kepala Kantor Kementerian Agama Kabupaten Muaro Jambi', 9),
(6, 'Kepala Kantor Kementerian Agama Kabupaten Sarolangun', 9),
(6, 'Kepala Kantor Kementerian Agama Kabupaten Tanjung Jabung Barat', 9),
(6, 'Kepala Kantor Kementerian Agama Kabupaten Tanjung Jabung Timur', 9),
(6, 'Kepala Kantor Kementerian Agama Kabupaten Tebo', 9),
(6, 'Kepala Kantor Kementerian Agama Kota Jambi', 9),
(6, 'Kepala Kantor Kementerian Agama Kota Sungai Penuh', 9),
(6, 'Kepala Kantor Regional VII BKN Palembang', 8),
(6, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(6, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(7, ' ', 9),
(7, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(7, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(7, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(7, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(7, 'Kantor Wilayah Kementerian Agama Provinsi Sumatera Selatan', 6),
(7, 'Kepala Kantor Kementerian Agama Kabupaten Banyuasin', 9),
(7, 'Kepala Kantor Kementerian Agama Kabupaten Empat Lawang', 9),
(7, 'Kepala Kantor Kementerian Agama Kabupaten Lahat', 9),
(7, 'Kepala Kantor Kementerian Agama Kabupaten Muara Enim', 9),
(7, 'Kepala Kantor Kementerian Agama Kabupaten Musi Banyuasin', 9),
(7, 'Kepala Kantor Kementerian Agama Kabupaten Musi Rawas', 9),
(7, 'Kepala Kantor Kementerian Agama Kabupaten Musi Rawas Utara', 9),
(7, 'Kepala Kantor Kementerian Agama Kabupaten Ogan Ilir', 9),
(7, 'Kepala Kantor Kementerian Agama Kabupaten Ogan Komering Ilir', 9),
(7, 'Kepala Kantor Kementerian Agama Kabupaten Ogan Komering Ulu', 9),
(7, 'Kepala Kantor Kementerian Agama Kabupaten OKU Selatan', 9),
(7, 'Kepala Kantor Kementerian Agama Kabupaten OKU Timur', 9),
(7, 'Kepala Kantor Kementerian Agama Kabupaten Penukang Abad Lematang Ilir', 9),
(7, 'Kepala Kantor Kementerian Agama Kota Lubuk Linggau', 9),
(7, 'Kepala Kantor Kementerian Agama Kota Pagar Alam', 9),
(7, 'Kepala Kantor Kementerian Agama Kota Palembang', 9),
(7, 'Kepala Kantor Kementerian Agama Kota Prabumulih', 9),
(7, 'Kepala Kantor Regional VII BKN Palembang', 8),
(7, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(7, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(8, ' ', 9),
(8, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(8, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(8, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(8, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(8, 'Kantor Wilayah Kementerian Agama Provinsi Bengkulu', 6),
(8, 'Kepala Kantor Kementerian Agama Kabupaten Bengkulu Selatan', 9),
(8, 'Kepala Kantor Kementerian Agama Kabupaten Bengkulu Tengah', 9),
(8, 'Kepala Kantor Kementerian Agama Kabupaten Bengkulu Utara', 9),
(8, 'Kepala Kantor Kementerian Agama Kabupaten Benteng', 9),
(8, 'Kepala Kantor Kementerian Agama Kabupaten Kaur', 9),
(8, 'Kepala Kantor Kementerian Agama Kabupaten Kepahiang', 9),
(8, 'Kepala Kantor Kementerian Agama Kabupaten Lebong', 9),
(8, 'Kepala Kantor Kementerian Agama Kabupaten Mukomuko', 9),
(8, 'Kepala Kantor Kementerian Agama Kabupaten Rejang Lebong', 9),
(8, 'Kepala Kantor Kementerian Agama Kabupaten Seluma', 9),
(8, 'Kepala Kantor Kementerian Agama Kota Bengkulu', 9),
(8, 'Kepala Kantor Regional VII BKN Palembang', 8),
(8, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(8, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(9, ' ', 9),
(9, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(9, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(9, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(9, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(9, 'Kantor Wilayah Kementerian Agama Provinsi Lampung', 6),
(9, 'Kepala Kantor Kementerian Agama Kabupaten Lampung Barat', 9),
(9, 'Kepala Kantor Kementerian Agama Kabupaten Lampung Selatan', 9),
(9, 'Kepala Kantor Kementerian Agama Kabupaten Lampung Tengah', 9),
(9, 'Kepala Kantor Kementerian Agama Kabupaten Lampung Timur', 9),
(9, 'Kepala Kantor Kementerian Agama Kabupaten Lampung Utara', 9),
(9, 'Kepala Kantor Kementerian Agama Kabupaten Mesuji', 9),
(9, 'Kepala Kantor Kementerian Agama Kabupaten Pesawaran', 9),
(9, 'Kepala Kantor Kementerian Agama Kabupaten Pringsewu', 9),
(9, 'Kepala Kantor Kementerian Agama Kabupaten Tanggamus', 9),
(9, 'Kepala Kantor Kementerian Agama Kabupaten Tulang Bawang', 9),
(9, 'Kepala Kantor Kementerian Agama Kabupaten Tulang Bawang Barat', 9),
(9, 'Kepala Kantor Kementerian Agama Kabupaten Way Kanan', 9),
(9, 'Kepala Kantor Kementerian Agama Kota Bandar Lampung', 9),
(9, 'Kepala Kantor Kementerian Agama Kota Metro', 9),
(9, 'Kepala Kantor Regional V BKN Jakarta', 8),
(9, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(9, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(10, ' ', 9),
(10, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(10, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(10, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(10, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(10, 'Kantor Wilayah Kementerian Agama Provinsi Kepulauan Bangka Belitung', 6),
(10, 'Kepala Kantor Kementerian Agama Kabupaten Bangka', 9),
(10, 'Kepala Kantor Kementerian Agama Kabupaten Bangka Barat', 9),
(10, 'Kepala Kantor Kementerian Agama Kabupaten Bangka Selatan', 9),
(10, 'Kepala Kantor Kementerian Agama Kabupaten Bangka Tengah', 9),
(10, 'Kepala Kantor Kementerian Agama Kabupaten Belitung', 9),
(10, 'Kepala Kantor Kementerian Agama Kabupaten Belitung Timur', 9),
(10, 'Kepala Kantor Kementerian Agama Kota Pangkalpinang', 9),
(10, 'Kepala Kantor Regional VII BKN Palembang', 8),
(10, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(10, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(11, ' ', 9),
(11, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(11, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(11, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(11, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(11, 'Kantor Wilayah Kementerian Agama Provinsi Banten', 6),
(11, 'Kepala Kantor Kementerian Agama Kabupaten Lebak', 9),
(11, 'Kepala Kantor Kementerian Agama Kabupaten Pandeglang', 9),
(11, 'Kepala Kantor Kementerian Agama Kabupaten Serang', 9),
(11, 'Kepala Kantor Kementerian Agama Kabupaten Tangerang', 9),
(11, 'Kepala Kantor Kementerian Agama Kota Cilegon', 9),
(11, 'Kepala Kantor Kementerian Agama Kota Serang', 9),
(11, 'Kepala Kantor Kementerian Agama Kota Tangerang', 9),
(11, 'Kepala Kantor Kementerian Agama Kota Tangerang Selatan', 9),
(11, 'Kepala Kantor Regional III BKN Bandung', 8),
(11, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(11, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(12, ' ', 9),
(12, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(12, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(12, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(12, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(12, 'Kantor Wilayah Kementerian Agama Provinsi DKI Jakarta', 6),
(12, 'Kepala Kantor Kementerian Agama Kabupaten Kepulauan Seribu', 9),
(12, 'Kepala Kantor Kementerian Agama Kota Jakarta Barat', 9),
(12, 'Kepala Kantor Kementerian Agama Kota Jakarta Pusat', 9),
(12, 'Kepala Kantor Kementerian Agama Kota Jakarta Selatan', 9),
(12, 'Kepala Kantor Kementerian Agama Kota Jakarta Timur', 9),
(12, 'Kepala Kantor Kementerian Agama Kota Jakarta Utara', 9),
(12, 'Kepala Kantor Regional V BKN Jakarta', 8),
(12, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(12, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(13, ' ', 9),
(13, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(13, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(13, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(13, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(13, 'Kantor Wilayah Kementerian Agama Provinsi Jawa Barat', 6),
(13, 'Kepala Kantor Kementerian Agama Kabupaten Bandung', 9),
(13, 'Kepala Kantor Kementerian Agama Kabupaten Bandung Barat', 9),
(13, 'Kepala Kantor Kementerian Agama Kabupaten Bekasi', 9),
(13, 'Kepala Kantor Kementerian Agama Kabupaten Bogor', 9),
(13, 'Kepala Kantor Kementerian Agama Kabupaten Ciamis', 9),
(13, 'Kepala Kantor Kementerian Agama Kabupaten Cianjur', 9),
(13, 'Kepala Kantor Kementerian Agama Kabupaten Cirebon', 9),
(13, 'Kepala Kantor Kementerian Agama Kabupaten Garut', 9),
(13, 'Kepala Kantor Kementerian Agama Kabupaten Indramayu', 9),
(13, 'Kepala Kantor Kementerian Agama Kabupaten Karawang', 9),
(13, 'Kepala Kantor Kementerian Agama Kabupaten Kuningan', 9),
(13, 'Kepala Kantor Kementerian Agama Kabupaten Majalengka', 9),
(13, 'Kepala Kantor Kementerian Agama Kabupaten Purwakarta', 9),
(13, 'Kepala Kantor Kementerian Agama Kabupaten Subang', 9),
(13, 'Kepala Kantor Kementerian Agama Kabupaten Sukabumi', 9),
(13, 'Kepala Kantor Kementerian Agama Kabupaten Sumedang', 9),
(13, 'Kepala Kantor Kementerian Agama Kabupaten Tasikmalaya', 9),
(13, 'Kepala Kantor Kementerian Agama Kota Bandung', 9),
(13, 'Kepala Kantor Kementerian Agama Kota Banjar', 9),
(13, 'Kepala Kantor Kementerian Agama Kota Bekasi', 9),
(13, 'Kepala Kantor Kementerian Agama Kota Bogor', 9),
(13, 'Kepala Kantor Kementerian Agama Kota Cimahi', 9),
(13, 'Kepala Kantor Kementerian Agama Kota Cirebon', 9),
(13, 'Kepala Kantor Kementerian Agama Kota Depok', 9),
(13, 'Kepala Kantor Kementerian Agama Kota Sukabumi', 9),
(13, 'Kepala Kantor Kementerian Agama Kota Tasikmalaya', 9),
(13, 'Kepala Kantor Regional III BKN Bandung', 8),
(13, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(13, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(14, ' ', 9),
(14, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(14, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(14, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(14, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(14, 'Kantor Wilayah Kementerian Agama Provinsi Jawa Tengah', 6),
(14, 'Kepala Kantor Kementerian Agama Kabupaten Banjarnegara', 9),
(14, 'Kepala Kantor Kementerian Agama Kabupaten Banyumas', 9),
(14, 'Kepala Kantor Kementerian Agama Kabupaten Batang', 9),
(14, 'Kepala Kantor Kementerian Agama Kabupaten Blora', 9),
(14, 'Kepala Kantor Kementerian Agama Kabupaten Boyolali', 9),
(14, 'Kepala Kantor Kementerian Agama Kabupaten Brebes', 9),
(14, 'Kepala Kantor Kementerian Agama Kabupaten Cilacap', 9),
(14, 'Kepala Kantor Kementerian Agama Kabupaten Demak', 9),
(14, 'Kepala Kantor Kementerian Agama Kabupaten Grobogan', 9),
(14, 'Kepala Kantor Kementerian Agama Kabupaten Jepara', 9),
(14, 'Kepala Kantor Kementerian Agama Kabupaten Karanganyar', 9),
(14, 'Kepala Kantor Kementerian Agama Kabupaten Kebumen', 9),
(14, 'Kepala Kantor Kementerian Agama Kabupaten Kendal', 9),
(14, 'Kepala Kantor Kementerian Agama Kabupaten Klaten', 9),
(14, 'Kepala Kantor Kementerian Agama Kabupaten Kudus', 9),
(14, 'Kepala Kantor Kementerian Agama Kabupaten Magelang', 9),
(14, 'Kepala Kantor Kementerian Agama Kabupaten Pati', 9),
(14, 'Kepala Kantor Kementerian Agama Kabupaten Pekalongan', 9),
(14, 'Kepala Kantor Kementerian Agama Kabupaten Pemalang', 9),
(14, 'Kepala Kantor Kementerian Agama Kabupaten Purbalingga', 9),
(14, 'Kepala Kantor Kementerian Agama Kabupaten Purworejo', 9),
(14, 'Kepala Kantor Kementerian Agama Kabupaten Rembang', 9),
(14, 'Kepala Kantor Kementerian Agama Kabupaten Semarang', 9),
(14, 'Kepala Kantor Kementerian Agama Kabupaten Sragen', 9),
(14, 'Kepala Kantor Kementerian Agama Kabupaten Sukoharjo', 9),
(14, 'Kepala Kantor Kementerian Agama Kabupaten Tegal', 9),
(14, 'Kepala Kantor Kementerian Agama Kabupaten Temanggung', 9),
(14, 'Kepala Kantor Kementerian Agama Kabupaten Wonogiri', 9),
(14, 'Kepala Kantor Kementerian Agama Kabupaten Wonosobo', 9),
(14, 'Kepala Kantor Kementerian Agama Kota Magelang', 9),
(14, 'Kepala Kantor Kementerian Agama Kota Pekalongan', 9),
(14, 'Kepala Kantor Kementerian Agama Kota Salatiga', 9),
(14, 'Kepala Kantor Kementerian Agama Kota Semarang', 9),
(14, 'Kepala Kantor Kementerian Agama Kota Surakarta', 9),
(14, 'Kepala Kantor Kementerian Agama Kota Tegal', 9),
(14, 'Kepala Kantor Regional I BKN Yogyakarta', 8),
(14, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(14, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(15, ' ', 9),
(15, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(15, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(15, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(15, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(15, 'Kantor Wilayah Kementerian Agama Provinsi Daerah Istimewa Yogyakarta', 6),
(15, 'Kepala Kantor Kementerian Agama Kabupaten Bantul', 9),
(15, 'Kepala Kantor Kementerian Agama Kabupaten Gunungkidul', 9),
(15, 'Kepala Kantor Kementerian Agama Kabupaten Kulon Progo', 9),
(15, 'Kepala Kantor Kementerian Agama Kabupaten Sleman', 9),
(15, 'Kepala Kantor Kementerian Agama Kota Yogyakarta', 9),
(15, 'Kepala Kantor Regional I BKN Yogyakarta', 8),
(15, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(15, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(16, ' ', 9),
(16, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(16, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(16, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(16, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(16, 'Kantor Wilayah Kementerian Agama Provinsi Jawa Timur', 6),
(16, 'Kepala Kantor Kementerian Agama Kabupaten Bangkalan', 9),
(16, 'Kepala Kantor Kementerian Agama Kabupaten Banyuwangi', 9),
(16, 'Kepala Kantor Kementerian Agama Kabupaten Blitar', 9),
(16, 'Kepala Kantor Kementerian Agama Kabupaten Bojonegoro', 9),
(16, 'Kepala Kantor Kementerian Agama Kabupaten Bondowoso', 9),
(16, 'Kepala Kantor Kementerian Agama Kabupaten Gresik', 9),
(16, 'Kepala Kantor Kementerian Agama Kabupaten Jember', 9),
(16, 'Kepala Kantor Kementerian Agama Kabupaten Jombang', 9),
(16, 'Kepala Kantor Kementerian Agama Kabupaten Kediri', 9),
(16, 'Kepala Kantor Kementerian Agama Kabupaten Lamongan', 9),
(16, 'Kepala Kantor Kementerian Agama Kabupaten Lumajang', 9),
(16, 'Kepala Kantor Kementerian Agama Kabupaten Madiun', 9),
(16, 'Kepala Kantor Kementerian Agama Kabupaten Magetan', 9),
(16, 'Kepala Kantor Kementerian Agama Kabupaten Malang', 9),
(16, 'Kepala Kantor Kementerian Agama Kabupaten Mojokerto', 9),
(16, 'Kepala Kantor Kementerian Agama Kabupaten Nganjuk', 9),
(16, 'Kepala Kantor Kementerian Agama Kabupaten Ngawi', 9),
(16, 'Kepala Kantor Kementerian Agama Kabupaten Pacitan', 9),
(16, 'Kepala Kantor Kementerian Agama Kabupaten Pamekasan', 9),
(16, 'Kepala Kantor Kementerian Agama Kabupaten Pasuruan', 9),
(16, 'Kepala Kantor Kementerian Agama Kabupaten Ponorogo', 9),
(16, 'Kepala Kantor Kementerian Agama Kabupaten Probolinggo', 9),
(16, 'Kepala Kantor Kementerian Agama Kabupaten Sampang', 9),
(16, 'Kepala Kantor Kementerian Agama Kabupaten Sidoarjo', 9),
(16, 'Kepala Kantor Kementerian Agama Kabupaten Situbondo', 9),
(16, 'Kepala Kantor Kementerian Agama Kabupaten Sumenep', 9),
(16, 'Kepala Kantor Kementerian Agama Kabupaten Trenggalek', 9),
(16, 'Kepala Kantor Kementerian Agama Kabupaten Tuban', 9),
(16, 'Kepala Kantor Kementerian Agama Kabupaten Tulungagung', 9),
(16, 'Kepala Kantor Kementerian Agama Kota Batu', 9),
(16, 'Kepala Kantor Kementerian Agama Kota Blitar', 9),
(16, 'Kepala Kantor Kementerian Agama Kota Kediri', 9),
(16, 'Kepala Kantor Kementerian Agama Kota Madiun', 9),
(16, 'Kepala Kantor Kementerian Agama Kota Malang', 9),
(16, 'Kepala Kantor Kementerian Agama Kota Mojokerto', 9),
(16, 'Kepala Kantor Kementerian Agama Kota Pasuruan', 9),
(16, 'Kepala Kantor Kementerian Agama Kota Probolinggo', 9),
(16, 'Kepala Kantor Kementerian Agama Kota Surabaya', 9),
(16, 'Kepala Kantor Regional II BKN Surabaya', 8),
(16, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(16, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(17, ' ', 9),
(17, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(17, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(17, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(17, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(17, 'Kantor Wilayah Kementerian Agama Provinsi Kalimantan Barat', 6),
(17, 'Kepala Kantor Kementerian Agama Kabupaten Bengkayang', 9),
(17, 'Kepala Kantor Kementerian Agama Kabupaten Kapuas Hulu', 9),
(17, 'Kepala Kantor Kementerian Agama Kabupaten Kayong Utara', 9),
(17, 'Kepala Kantor Kementerian Agama Kabupaten Ketapang', 9),
(17, 'Kepala Kantor Kementerian Agama Kabupaten Kubu Raya', 9),
(17, 'Kepala Kantor Kementerian Agama Kabupaten Landak', 9),
(17, 'Kepala Kantor Kementerian Agama Kabupaten Melawi', 9),
(17, 'Kepala Kantor Kementerian Agama Kabupaten Pontianak', 9),
(17, 'Kepala Kantor Kementerian Agama Kabupaten Sambas', 9),
(17, 'Kepala Kantor Kementerian Agama Kabupaten Sanggau', 9),
(17, 'Kepala Kantor Kementerian Agama Kabupaten Sekadau', 9),
(17, 'Kepala Kantor Kementerian Agama Kabupaten Sintang', 9),
(17, 'Kepala Kantor Kementerian Agama Kota Pontianak', 9),
(17, 'Kepala Kantor Kementerian Agama Kota Singkawang', 9),
(17, 'Kepala Kantor Regional V BKN Jakarta', 8),
(17, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(17, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(18, ' ', 9),
(18, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(18, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(18, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(18, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(18, 'Kantor Wilayah Kementerian Agama Provinsi Kalimantan Tengah', 6),
(18, 'Kepala Kantor Kementerian Agama Kabupaten Barito Selatan', 9),
(18, 'Kepala Kantor Kementerian Agama Kabupaten Barito Timur', 9),
(18, 'Kepala Kantor Kementerian Agama Kabupaten Barito Utara', 9),
(18, 'Kepala Kantor Kementerian Agama Kabupaten Gunung Mas', 9),
(18, 'Kepala Kantor Kementerian Agama Kabupaten Kapuas', 9),
(18, 'Kepala Kantor Kementerian Agama Kabupaten Kasongan', 9),
(18, 'Kepala Kantor Kementerian Agama Kabupaten Katingan', 9),
(18, 'Kepala Kantor Kementerian Agama Kabupaten Kotawaringin Barat', 9),
(18, 'Kepala Kantor Kementerian Agama Kabupaten Kotawaringin Timur', 9),
(18, 'Kepala Kantor Kementerian Agama Kabupaten Lamandau', 9),
(18, 'Kepala Kantor Kementerian Agama Kabupaten Murung Raya', 9),
(18, 'Kepala Kantor Kementerian Agama Kabupaten Pulang Pisau', 9),
(18, 'Kepala Kantor Kementerian Agama Kabupaten Seruyan', 9),
(18, 'Kepala Kantor Kementerian Agama Kabupaten Sukamara', 9),
(18, 'Kepala Kantor Kementerian Agama Kota Palangka Raya', 9),
(18, 'Kepala Kantor Regional VIII BKN Banjarmasin', 8),
(18, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(18, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(19, ' ', 9),
(19, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(19, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(19, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(19, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(19, 'Kantor Wilayah Kementerian Agama Provinsi Kalimantan Selatan', 6),
(19, 'Kepala Kantor Kementerian Agama Kabupaten Balangan', 9),
(19, 'Kepala Kantor Kementerian Agama Kabupaten Banjar', 9),
(19, 'Kepala Kantor Kementerian Agama Kabupaten Barito Kuala', 9),
(19, 'Kepala Kantor Kementerian Agama Kabupaten Hulu Sungai Selatan', 9),
(19, 'Kepala Kantor Kementerian Agama Kabupaten Hulu Sungai Tengah', 9),
(19, 'Kepala Kantor Kementerian Agama Kabupaten Hulu Sungai Utara', 9),
(19, 'Kepala Kantor Kementerian Agama Kabupaten Kotabaru', 9),
(19, 'Kepala Kantor Kementerian Agama Kabupaten Tabalong', 9),
(19, 'Kepala Kantor Kementerian Agama Kabupaten Tanah Bumbu', 9),
(19, 'Kepala Kantor Kementerian Agama Kabupaten Tanah Laut', 9),
(19, 'Kepala Kantor Kementerian Agama Kabupaten Tapin', 9),
(19, 'Kepala Kantor Kementerian Agama Kota Banjarbaru', 9),
(19, 'Kepala Kantor Kementerian Agama Kota Banjarmasin', 9),
(19, 'Kepala Kantor Regional VIII BKN Banjarmasin', 8),
(19, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(19, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(20, ' ', 9),
(20, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(20, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(20, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(20, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(20, 'Kantor Wilayah Kementerian Agama Provinsi Kalimantan Timur', 6),
(20, 'Kepala Kantor Kementerian Agama Kabupaten Berau', 9),
(20, 'Kepala Kantor Kementerian Agama Kabupaten Bulungan', 9),
(20, 'Kepala Kantor Kementerian Agama Kabupaten Kutai Barat', 9),
(20, 'Kepala Kantor Kementerian Agama Kabupaten Kutai Kertanegara', 9),
(20, 'Kepala Kantor Kementerian Agama Kabupaten Kutai Timur', 9),
(20, 'Kepala Kantor Kementerian Agama Kabupaten Malinau', 9),
(20, 'Kepala Kantor Kementerian Agama Kabupaten Nunukan', 9),
(20, 'Kepala Kantor Kementerian Agama Kabupaten Pasir', 9),
(20, 'Kepala Kantor Kementerian Agama Kabupaten Penajam Paser Utara', 9),
(20, 'Kepala Kantor Kementerian Agama Kabupaten Tana Tidung', 9),
(20, 'Kepala Kantor Kementerian Agama Kota Balikpapan', 9),
(20, 'Kepala Kantor Kementerian Agama Kota Bontang', 9),
(20, 'Kepala Kantor Kementerian Agama Kota Samarinda', 9),
(20, 'Kepala Kantor Kementerian Agama Kota Tarakan', 9),
(20, 'Kepala Kantor Regional VIII BKN Banjarmasin', 8),
(20, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(20, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(21, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(21, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(21, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(21, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(21, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(21, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(22, ' ', 9),
(22, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(22, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(22, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(22, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(22, 'Kantor Wilayah Kementerian Agama Provinsi Sulawesi Utara', 6),
(22, 'Kepala Kantor Kementerian Agama Kabupaten Bolaang Mangondow Selatan', 9),
(22, 'Kepala Kantor Kementerian Agama Kabupaten Bolaang Mangondow Timur', 9),
(22, 'Kepala Kantor Kementerian Agama Kabupaten Bolaang Mangondow Utara', 9),
(22, 'Kepala Kantor Kementerian Agama Kabupaten Bolaang Mongondow', 9),
(22, 'Kepala Kantor Kementerian Agama Kabupaten Kepulauan Sangihe', 9),
(22, 'Kepala Kantor Kementerian Agama Kabupaten Kepulauan Sitaro', 9),
(22, 'Kepala Kantor Kementerian Agama Kabupaten Kepulauan Talaud', 9),
(22, 'Kepala Kantor Kementerian Agama Kabupaten Minahasa', 9),
(22, 'Kepala Kantor Kementerian Agama Kabupaten Minahasa Selatan', 9),
(22, 'Kepala Kantor Kementerian Agama Kabupaten Minahasa Tenggara', 9),
(22, 'Kepala Kantor Kementerian Agama Kabupaten Minahasa Utara', 9),
(22, 'Kepala Kantor Kementerian Agama Kota Bitung', 9),
(22, 'Kepala Kantor Kementerian Agama Kota Kotamobagu', 9),
(22, 'Kepala Kantor Kementerian Agama Kota Manado', 9),
(22, 'Kepala Kantor Kementerian Agama Kota Tomohon', 9),
(22, 'Kepala Kantor Regional XI BKN Manado', 8),
(22, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(22, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(23, ' ', 9),
(23, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(23, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(23, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(23, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(23, 'Kantor Wilayah Kementerian Agama Provinsi Gorontalo', 6),
(23, 'Kepala Kantor Kementerian Agama Kabupaten Boalemo', 9),
(23, 'Kepala Kantor Kementerian Agama Kabupaten Bone Bolango', 9),
(23, 'Kepala Kantor Kementerian Agama Kabupaten Gorontalo', 9),
(23, 'Kepala Kantor Kementerian Agama Kabupaten Gorontalo Utara', 9),
(23, 'Kepala Kantor Kementerian Agama Kabupaten Pohuwato', 9),
(23, 'Kepala Kantor Kementerian Agama Kota Gorontalo', 9),
(23, 'Kepala Kantor Regional XI BKN Manado', 8),
(23, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(23, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(24, ' ', 9),
(24, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(24, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(24, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(24, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(24, 'Kantor Wilayah Kementerian Agama Provinsi Sulawesi Tengah', 6),
(24, 'Kepala Kantor Kementerian Agama Kabupaten Banggai', 9),
(24, 'Kepala Kantor Kementerian Agama Kabupaten Banggai Kepulauan', 9),
(24, 'Kepala Kantor Kementerian Agama Kabupaten Buol', 9),
(24, 'Kepala Kantor Kementerian Agama Kabupaten Donggala', 9),
(24, 'Kepala Kantor Kementerian Agama Kabupaten Morowali', 9),
(24, 'Kepala Kantor Kementerian Agama Kabupaten Parigi Moutong', 9),
(24, 'Kepala Kantor Kementerian Agama Kabupaten Poso', 9),
(24, 'Kepala Kantor Kementerian Agama Kabupaten Sigi', 9),
(24, 'Kepala Kantor Kementerian Agama Kabupaten Tojo Una-Una', 9),
(24, 'Kepala Kantor Kementerian Agama Kabupaten Toli-Toli', 9),
(24, 'Kepala Kantor Kementerian Agama Kota Palu', 9),
(24, 'Kepala Kantor Regional IV BKN Makassar', 8),
(24, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(24, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(25, ' ', 9),
(25, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(25, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(25, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(25, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(25, 'Kantor Wilayah Kementerian Agama Provinsi Sulawesi Tenggara', 6),
(25, 'Kepala Kantor Kementerian Agama Kabupaten Bombana', 9),
(25, 'Kepala Kantor Kementerian Agama Kabupaten Buton', 9),
(25, 'Kepala Kantor Kementerian Agama Kabupaten Buton Utara', 9),
(25, 'Kepala Kantor Kementerian Agama Kabupaten Kendari', 9),
(25, 'Kepala Kantor Kementerian Agama Kabupaten Kolaka', 9),
(25, 'Kepala Kantor Kementerian Agama Kabupaten Kolaka Utara', 9),
(25, 'Kepala Kantor Kementerian Agama Kabupaten Konawe', 9),
(25, 'Kepala Kantor Kementerian Agama Kabupaten Konawe Selatan', 9),
(25, 'Kepala Kantor Kementerian Agama Kabupaten Muna', 9),
(25, 'Kepala Kantor Kementerian Agama Kabupaten Wakatobi', 9),
(25, 'Kepala Kantor Kementerian Agama Kota Bau-bau', 9),
(25, 'Kepala Kantor Kementerian Agama Kota Kendari', 9),
(25, 'Kepala Kantor Regional IV BKN Makassar', 8),
(25, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(25, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(26, ' ', 9),
(26, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(26, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(26, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(26, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(26, 'Kantor Wilayah Kementerian Agama Provinsi Sulawesi Barat', 6),
(26, 'Kepala Kantor Kementerian Agama Kabupaten Majene', 9),
(26, 'Kepala Kantor Kementerian Agama Kabupaten Mamasa', 9),
(26, 'Kepala Kantor Kementerian Agama Kabupaten Mamuju', 9),
(26, 'Kepala Kantor Kementerian Agama Kabupaten Mamuju Utara', 9),
(26, 'Kepala Kantor Kementerian Agama Kabupaten Polewali Mamasa', 9),
(26, 'Kepala Kantor Kementerian Agama Kabupaten Polewali Mandar', 9),
(26, 'Kepala Kantor Regional IV BKN Makassar', 8),
(26, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(26, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(27, ' ', 9),
(27, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(27, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(27, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(27, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(27, 'Kantor Wilayah Kementerian Agama Provinsi Sulawesi Selatan', 6),
(27, 'Kepala Kantor Kementerian Agama Kabupaten Bantaeng', 9),
(27, 'Kepala Kantor Kementerian Agama Kabupaten Barru', 9),
(27, 'Kepala Kantor Kementerian Agama Kabupaten Bone', 9),
(27, 'Kepala Kantor Kementerian Agama Kabupaten Bulukumba', 9),
(27, 'Kepala Kantor Kementerian Agama Kabupaten Enrekang', 9),
(27, 'Kepala Kantor Kementerian Agama Kabupaten Gowa', 9),
(27, 'Kepala Kantor Kementerian Agama Kabupaten Jeneponto', 9),
(27, 'Kepala Kantor Kementerian Agama Kabupaten Luwu', 9),
(27, 'Kepala Kantor Kementerian Agama Kabupaten Luwu Timur', 9),
(27, 'Kepala Kantor Kementerian Agama Kabupaten Luwu Utara', 9),
(27, 'Kepala Kantor Kementerian Agama Kabupaten Maros', 9),
(27, 'Kepala Kantor Kementerian Agama Kabupaten Pangkajene Kepulauan', 9),
(27, 'Kepala Kantor Kementerian Agama Kabupaten Pinrang', 9),
(27, 'Kepala Kantor Kementerian Agama Kabupaten Selayar', 9),
(27, 'Kepala Kantor Kementerian Agama Kabupaten Sengkang', 9),
(27, 'Kepala Kantor Kementerian Agama Kabupaten Sidenreng Rappang', 9),
(27, 'Kepala Kantor Kementerian Agama Kabupaten Sinjai', 9),
(27, 'Kepala Kantor Kementerian Agama Kabupaten Soppeng', 9),
(27, 'Kepala Kantor Kementerian Agama Kabupaten Takalar', 9),
(27, 'Kepala Kantor Kementerian Agama Kabupaten Tana Toraja', 9),
(27, 'Kepala Kantor Kementerian Agama Kabupaten Toraja Utara', 9),
(27, 'Kepala Kantor Kementerian Agama Kabupaten Wajo', 9),
(27, 'Kepala Kantor Kementerian Agama Kota Makassar', 9),
(27, 'Kepala Kantor Kementerian Agama Kota Palopo', 9),
(27, 'Kepala Kantor Kementerian Agama Kota Parepare', 9),
(27, 'Kepala Kantor Regional IV BKN Makassar', 8),
(27, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(27, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(28, ' ', 9),
(28, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(28, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(28, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(28, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(28, 'Kantor Wilayah Kementerian Agama Provinsi Bali', 6),
(28, 'Kepala Kantor Kementerian Agama Kabupaten Badung', 9),
(28, 'Kepala Kantor Kementerian Agama Kabupaten Bangli', 9),
(28, 'Kepala Kantor Kementerian Agama Kabupaten Buleleng', 9),
(28, 'Kepala Kantor Kementerian Agama Kabupaten Gianyar', 9),
(28, 'Kepala Kantor Kementerian Agama Kabupaten Jembrana', 9),
(28, 'Kepala Kantor Kementerian Agama Kabupaten Karangasem', 9),
(28, 'Kepala Kantor Kementerian Agama Kabupaten Klungkung', 9),
(28, 'Kepala Kantor Kementerian Agama Kabupaten Tabanan', 9),
(28, 'Kepala Kantor Kementerian Agama Kota Denpasar', 9),
(28, 'Kepala Kantor Regional X BKN Denpasar', 8),
(28, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(28, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(29, ' ', 9),
(29, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(29, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(29, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(29, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(29, 'Kantor Wilayah Kementerian Agama Provinsi Nusa Tenggara Barat', 6),
(29, 'Kepala Kantor Kementerian Agama Kabupaten Bima', 9),
(29, 'Kepala Kantor Kementerian Agama Kabupaten Dompu', 9),
(29, 'Kepala Kantor Kementerian Agama Kabupaten Lombok Barat', 9),
(29, 'Kepala Kantor Kementerian Agama Kabupaten Lombok Tengah', 9),
(29, 'Kepala Kantor Kementerian Agama Kabupaten Lombok Timur', 9),
(29, 'Kepala Kantor Kementerian Agama Kabupaten Lombok Utara', 9),
(29, 'Kepala Kantor Kementerian Agama Kabupaten Sumbawa', 9),
(29, 'Kepala Kantor Kementerian Agama Kabupaten Sumbawa Barat', 9),
(29, 'Kepala Kantor Kementerian Agama Kota Bima', 9),
(29, 'Kepala Kantor Kementerian Agama Kota Mataram', 9),
(29, 'Kepala Kantor Regional X BKN Denpasar', 8),
(29, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(29, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(30, ' ', 9),
(30, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(30, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(30, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(30, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(30, 'Kantor Wilayah Kementerian Agama Provinsi Nusa Tenggara Timur', 6),
(30, 'Kepala Kantor Kementerian Agama Kabupaten Alor', 9),
(30, 'Kepala Kantor Kementerian Agama Kabupaten Belu', 9),
(30, 'Kepala Kantor Kementerian Agama Kabupaten Ende', 9),
(30, 'Kepala Kantor Kementerian Agama Kabupaten Flores Timur', 9),
(30, 'Kepala Kantor Kementerian Agama Kabupaten Kabupaten Manggarai Timur', 9),
(30, 'Kepala Kantor Kementerian Agama Kabupaten Kabupaten Nagekeo', 9),
(30, 'Kepala Kantor Kementerian Agama Kabupaten Kabupaten Sabu Raijua', 9),
(30, 'Kepala Kantor Kementerian Agama Kabupaten Kabupaten Sumba Barat Daya', 9),
(30, 'Kepala Kantor Kementerian Agama Kabupaten Kabupaten Sumba Tengah', 9),
(30, 'Kepala Kantor Kementerian Agama Kabupaten Kupang', 9),
(30, 'Kepala Kantor Kementerian Agama Kabupaten Lembata', 9),
(30, 'Kepala Kantor Kementerian Agama Kabupaten Manggarai', 9),
(30, 'Kepala Kantor Kementerian Agama Kabupaten Manggarai Barat', 9),
(30, 'Kepala Kantor Kementerian Agama Kabupaten Ngada', 9),
(30, 'Kepala Kantor Kementerian Agama Kabupaten Rote Ndao', 9),
(30, 'Kepala Kantor Kementerian Agama Kabupaten Sikka', 9),
(30, 'Kepala Kantor Kementerian Agama Kabupaten Sumba Barat', 9),
(30, 'Kepala Kantor Kementerian Agama Kabupaten Sumba Timur', 9),
(30, 'Kepala Kantor Kementerian Agama Kabupaten Timor Tengah Selatan', 9),
(30, 'Kepala Kantor Kementerian Agama Kabupaten Timor Tengah Utara', 9),
(30, 'Kepala Kantor Kementerian Agama Kota Kupang', 9),
(30, 'Kepala Kantor Regional X BKN Denpasar', 8),
(30, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(30, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(31, ' ', 9),
(31, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(31, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(31, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(31, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(31, 'Kantor Wilayah Kementerian Agama Provinsi Maluku', 6),
(31, 'Kepala Kantor Kementerian Agama Kabupaten Buru', 9),
(31, 'Kepala Kantor Kementerian Agama Kabupaten Buru Selatan', 9),
(31, 'Kepala Kantor Kementerian Agama Kabupaten Kepulauan Aru', 9),
(31, 'Kepala Kantor Kementerian Agama Kabupaten Maluku Barat Daya', 9),
(31, 'Kepala Kantor Kementerian Agama Kabupaten Maluku Tengah', 9),
(31, 'Kepala Kantor Kementerian Agama Kabupaten Maluku Tenggara', 9),
(31, 'Kepala Kantor Kementerian Agama Kabupaten Maluku Tenggara Barat', 9),
(31, 'Kepala Kantor Kementerian Agama Kabupaten Seram Bagian Barat', 9),
(31, 'Kepala Kantor Kementerian Agama Kabupaten Seram Bagian Timur', 9),
(31, 'Kepala Kantor Kementerian Agama Kota Ambon', 9),
(31, 'Kepala Kantor Kementerian Agama Kota Tual', 9),
(31, 'Kepala Kantor Regional IV BKN Makassar', 8),
(31, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(31, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(32, ' ', 9),
(32, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(32, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(32, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(32, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(32, 'Kantor Wilayah Kementerian Agama Provinsi Maluku Utara', 6),
(32, 'Kepala Kantor Kementerian Agama Kabupaten Halmahera Barat', 9),
(32, 'Kepala Kantor Kementerian Agama Kabupaten Halmahera Selatan', 9),
(32, 'Kepala Kantor Kementerian Agama Kabupaten Halmahera Tengah', 9),
(32, 'Kepala Kantor Kementerian Agama Kabupaten Halmahera Timur', 9),
(32, 'Kepala Kantor Kementerian Agama Kabupaten Halmahera Utara', 9),
(32, 'Kepala Kantor Kementerian Agama Kabupaten Kepulauan Sula', 9),
(32, 'Kepala Kantor Kementerian Agama Kabupaten Pulau Morotai', 9),
(32, 'Kepala Kantor Kementerian Agama Kota Ternate', 9),
(32, 'Kepala Kantor Kementerian Agama Kota Tidore Kepulauan', 9),
(32, 'Kepala Kantor Regional XI BKN Manado', 8),
(32, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(32, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(33, ' ', 9),
(33, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(33, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(33, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(33, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(33, 'Kantor Wilayah Kementerian Agama Provinsi Papua', 6),
(33, 'Kepala Kantor Kementerian Agama Kabupaten Asmat', 9),
(33, 'Kepala Kantor Kementerian Agama Kabupaten Biak Numfor', 9),
(33, 'Kepala Kantor Kementerian Agama Kabupaten Boven Digoel', 9),
(33, 'Kepala Kantor Kementerian Agama Kabupaten Deiyai', 9),
(33, 'Kepala Kantor Kementerian Agama Kabupaten Dogiyai', 9),
(33, 'Kepala Kantor Kementerian Agama Kabupaten Intan Jaya', 9),
(33, 'Kepala Kantor Kementerian Agama Kabupaten Jayapura', 9),
(33, 'Kepala Kantor Kementerian Agama Kabupaten Jayawijaya', 9),
(33, 'Kepala Kantor Kementerian Agama Kabupaten Keerom', 9),
(33, 'Kepala Kantor Kementerian Agama Kabupaten Kepulauan Yapen', 9),
(33, 'Kepala Kantor Kementerian Agama Kabupaten Lanny Jaya', 9),
(33, 'Kepala Kantor Kementerian Agama Kabupaten Mamberamo Raya', 9),
(33, 'Kepala Kantor Kementerian Agama Kabupaten Mamberamo Tengah', 9),
(33, 'Kepala Kantor Kementerian Agama Kabupaten Mappi', 9),
(33, 'Kepala Kantor Kementerian Agama Kabupaten Merauke', 9),
(33, 'Kepala Kantor Kementerian Agama Kabupaten Mimika', 9),
(33, 'Kepala Kantor Kementerian Agama Kabupaten Nabire', 9),
(33, 'Kepala Kantor Kementerian Agama Kabupaten Nduga', 9),
(33, 'Kepala Kantor Kementerian Agama Kabupaten Paniai', 9),
(33, 'Kepala Kantor Kementerian Agama Kabupaten Pengunungan Bintang', 9);
INSERT INTO `tbl_tembusan` (`KODE_SATUAN_KERJA`, `Tembusan`, `Tipe`) VALUES
(33, 'Kepala Kantor Kementerian Agama Kabupaten Puncak', 9),
(33, 'Kepala Kantor Kementerian Agama Kabupaten Puncak Jaya', 9),
(33, 'Kepala Kantor Kementerian Agama Kabupaten Sarmi', 9),
(33, 'Kepala Kantor Kementerian Agama Kabupaten Supiori', 9),
(33, 'Kepala Kantor Kementerian Agama Kabupaten Tolikara', 9),
(33, 'Kepala Kantor Kementerian Agama Kabupaten Waropen', 9),
(33, 'Kepala Kantor Kementerian Agama Kabupaten Yahukimo', 9),
(33, 'Kepala Kantor Kementerian Agama Kabupaten Yalimo', 9),
(33, 'Kepala Kantor Kementerian Agama Kota Jayapura', 9),
(33, 'Kepala Kantor Regional IX BKN Jayapura', 8),
(33, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(33, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(34, ' ', 9),
(34, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(34, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(34, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(34, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(34, 'Kantor Wilayah Kementerian Agama Provinsi Papua Barat', 6),
(34, 'Kepala Kantor Kementerian Agama Kabupaten Fak-Fak', 9),
(34, 'Kepala Kantor Kementerian Agama Kabupaten Kaimana', 9),
(34, 'Kepala Kantor Kementerian Agama Kabupaten Manokwari', 9),
(34, 'Kepala Kantor Kementerian Agama Kabupaten Maybrat', 9),
(34, 'Kepala Kantor Kementerian Agama Kabupaten Raja Ampat', 9),
(34, 'Kepala Kantor Kementerian Agama Kabupaten Sorong', 9),
(34, 'Kepala Kantor Kementerian Agama Kabupaten Sorong Selatan', 9),
(34, 'Kepala Kantor Kementerian Agama Kabupaten Tambrauw', 9),
(34, 'Kepala Kantor Kementerian Agama Kabupaten Teluk Bentuni', 9),
(34, 'Kepala Kantor Kementerian Agama Kabupaten Teluk Wondama', 9),
(34, 'Kepala Kantor Kementerian Agama Kota Sorong', 9),
(34, 'Kepala Kantor Regional XIV BKN Manokwari', 8),
(34, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(34, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(35, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(35, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(35, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(35, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(35, 'Kepala Kantor Regional VI BKN Medan', 8),
(35, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(35, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(35, 'Universitas Islam Negeri Ar-Raniry Banda Aceh', 6),
(36, ' ', 9),
(36, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(36, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(36, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(36, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(36, 'Kepala Kantor Regional XII BKN Pekanbaru', 8),
(36, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(36, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(36, 'Universitas Islam Negeri Sultan Syarif Kasim Riau', 6),
(37, ' ', 9),
(37, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(37, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(37, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(37, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(37, 'Kepala Kantor Regional V BKN Jakarta', 8),
(37, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(37, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(37, 'Universitas Islam Negeri Syarif Hidayatullah Jakarta', 6),
(38, ' ', 9),
(38, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(38, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(38, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(38, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(38, 'Kepala Kantor Regional III BKN Bandung', 8),
(38, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(38, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(38, 'Universitas Islam Negeri Sunan Gunung Djati Bandung', 6),
(39, ' ', 9),
(39, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(39, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(39, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(39, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(39, 'Kepala Kantor Regional I BKN Yogyakarta', 8),
(39, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(39, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(39, 'Universitas Islam Negeri Sunan Kalijaga Yogyakarta', 6),
(40, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(40, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(40, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(40, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(40, 'Kepala Kantor Regional II BKN Surabaya', 8),
(40, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(40, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(40, 'Universitas Islam Negeri Sunan Ampel Surabaya', 6),
(41, ' ', 9),
(41, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(41, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(41, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(41, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(41, 'Kepala Kantor Regional II BKN Surabaya', 8),
(41, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(41, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(41, 'Universitas Islam Negeri Maulana Malik Ibrahim Malang', 6),
(42, ' ', 9),
(42, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(42, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(42, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(42, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(42, 'Kepala Kantor Regional IV BKN Makassar', 8),
(42, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(42, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(42, 'Universitas Islam Negeri Alauddin Makassar', 6),
(43, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(43, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(43, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(43, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(43, 'Institut Agama Islam Negeri Sumatera Utara', 6),
(43, 'Kepala Kantor Regional VI BKN Medan', 8),
(43, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(43, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(44, ' ', 9),
(44, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(44, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(44, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(44, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(44, 'Institut Agama Islam Negeri Padangsidimpuan', 6),
(44, 'Kepala Kantor Regional VI BKN Medan', 8),
(44, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(44, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(45, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(45, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(45, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(45, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(45, 'Institut Agama Islam Negeri Imam Bonjol Padang', 6),
(45, 'Kepala Kantor Regional XII BKN Pekanbaru', 8),
(45, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(45, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(46, ' ', 9),
(46, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(46, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(46, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(46, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(46, 'Institut Agama Islam Negeri Bengkulu', 6),
(46, 'Kepala Kantor Regional VII BKN Palembang', 8),
(46, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(46, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(47, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(47, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(47, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(47, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(47, 'Institut Agama Islam Negeri Sulthan Thaha Saifuddin Jambi', 6),
(47, 'Kepala Kantor Regional VII BKN Palembang', 8),
(47, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(47, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(48, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(48, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(48, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(48, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(48, 'Institut Agama Islam Negeri Raden Fatah Palembang', 6),
(48, 'Kepala Kantor Regional VII BKN Palembang', 8),
(48, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(48, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(49, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(49, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(49, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(49, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(49, 'Institut Agama Islam Negeri Raden Intan Lampung', 6),
(49, 'Kepala Kantor Regional V BKN Jakarta', 8),
(49, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(49, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(50, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(50, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(50, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(50, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(50, 'Institut Agama Islam Negeri Sultan Maulana Hasanuddin Banten', 6),
(50, 'Kepala Kantor Regional III BKN Bandung', 8),
(50, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(50, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(51, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(51, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(51, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(51, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(51, 'Institut Agama Islam Negeri Syekh Nurjati Cirebon', 6),
(51, 'Kepala Kantor Regional III BKN Bandung', 8),
(51, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(51, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(52, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(52, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(52, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(52, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(52, 'Institut Agama Islam Negeri Walisongo Semarang', 6),
(52, 'Kepala Kantor Regional I BKN Yogyakarta', 8),
(52, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(52, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(53, ' ', 9),
(53, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(53, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(53, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(53, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(53, 'Institut Agama Islam Negeri Surakarta', 6),
(53, 'Kepala Kantor Regional I BKN Yogyakarta', 8),
(53, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(53, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(54, ' ', 9),
(54, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(54, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(54, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(54, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(54, 'Institut Agama Islam Negeri Tulungagung', 6),
(54, 'Kepala Kantor Regional II BKN Surabaya', 8),
(54, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(54, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(55, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(55, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(55, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(55, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(55, 'Institut Agama Islam Negeri Antasari Banjarmasin', 6),
(55, 'Kepala Kantor Regional VIII BKN Banjarmasin', 8),
(55, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(55, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(56, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(56, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(56, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(56, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(56, 'Institut Agama Islam Negeri Sultan Amai Gorontalo', 6),
(56, 'Kepala Kantor Regional XI BKN Manado', 8),
(56, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(56, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(57, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(57, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(57, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(57, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(57, 'Institut Agama Islam Negeri Ambon', 6),
(57, 'Kepala Kantor Regional IV BKN Makassar', 8),
(57, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(57, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(58, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(58, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(58, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(58, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(58, 'Institut Agama Islam Negeri Mataram', 6),
(58, 'Kepala Kantor Regional X BKN Denpasar', 8),
(58, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(58, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(59, ' ', 9),
(59, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(59, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(59, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(59, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(59, 'Kepala Kantor Regional V BKN Jakarta', 8),
(59, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(59, 'Sekolah Tinggi Agama Islam Negeri Pontianak', 6),
(59, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(60, ' ', 9),
(60, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(60, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(60, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(60, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(60, 'Institut Agama Islam Negeri Datokarama Palu', 6),
(60, 'Kepala Kantor Regional IV BKN Makassar', 8),
(60, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(60, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(61, ' ', 9),
(61, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(61, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(61, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(61, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(61, 'Kepala Kantor Regional XI BKN Manado', 8),
(61, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(61, 'Sekolah Tinggi Agama Islam Negeri Ternate', 6),
(61, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(62, ' ', 9),
(62, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(62, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(62, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(62, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(62, 'Kepala Kantor Regional XII BKN Pekanbaru', 8),
(62, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(62, 'Sekolah Tinggi Agama Islam Negeri Sjech M. Djamil Djambek Bukittinggi', 6),
(62, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(63, ' ', 9),
(63, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(63, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(63, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(63, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(63, 'Kepala Kantor Regional VI BKN Medan', 8),
(63, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(63, 'Sekolah Tinggi Agama Islam Negeri Malikussaleh Lhokseumawe', 6),
(63, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(64, ' ', 9),
(64, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(64, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(64, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(64, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(64, 'Kepala Kantor Regional VI BKN Medan', 8),
(64, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(64, 'Sekolah Tinggi Agama Islam Negeri Zawiyah Cot Kala Langsa', 6),
(64, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(65, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(65, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(65, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(65, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(65, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(65, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(66, ' ', 9),
(66, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(66, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(66, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(66, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(66, 'Kepala Kantor Regional XII BKN Pekanbaru', 8),
(66, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(66, 'Sekolah Tinggi Agama Islam Negeri Batusangkar', 6),
(66, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(67, ' ', 9),
(67, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(67, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(67, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(67, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(67, 'Kepala Kantor Regional VII BKN Palembang', 8),
(67, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(67, 'Sekolah Tinggi Agama Islam Negeri Kerinci', 6),
(67, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(68, ' ', 9),
(68, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(68, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(68, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(68, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(68, 'Kepala Kantor Regional VII BKN Palembang', 8),
(68, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(68, 'Sekolah Tinggi Agama Islam Negeri Curup', 6),
(68, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(69, ' ', 9),
(69, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(69, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(69, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(69, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(69, 'Kepala Kantor Regional VII BKN Palembang', 8),
(69, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(69, 'Sekolah Tinggi Agama Islam Negeri Syaikh Abdurrahman Siddik Bangka Belitung', 6),
(69, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(70, ' ', 9),
(70, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(70, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(70, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(70, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(70, 'Kepala Kantor Regional V BKN Jakarta', 8),
(70, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(70, 'Sekolah Tinggi Agama Islam Negeri Jurai Siwo Metro', 6),
(70, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(71, ' ', 9),
(71, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(71, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(71, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(71, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(71, 'Kepala Kantor Regional I BKN Yogyakarta', 8),
(71, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(71, 'Sekolah Tinggi Agama Islam Negeri Kudus', 6),
(71, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(72, ' ', 9),
(72, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(72, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(72, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(72, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(72, 'Kepala Kantor Regional I BKN Yogyakarta', 8),
(72, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(72, 'Sekolah Tinggi Agama Islam Negeri Pekalongan', 6),
(72, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(73, ' ', 9),
(73, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(73, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(73, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(73, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(73, 'Kepala Kantor Regional I BKN Yogyakarta', 8),
(73, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(73, 'Sekolah Tinggi Agama Islam Negeri Purwokerto', 6),
(73, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(74, ' ', 9),
(74, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(74, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(74, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(74, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(74, 'Kepala Kantor Regional I BKN Yogyakarta', 8),
(74, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(74, 'Sekolah Tinggi Agama Islam Negeri Salatiga', 6),
(74, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(75, ' ', 9),
(75, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(75, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(75, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(75, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(75, 'Kepala Kantor Regional II BKN Surabaya', 8),
(75, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(75, 'Sekolah Tinggi Agama Islam Negeri Jember', 6),
(75, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(76, ' ', 9),
(76, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(76, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(76, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(76, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(76, 'Kepala Kantor Regional II BKN Surabaya', 8),
(76, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(76, 'Sekolah Tinggi Agama Islam Negeri Kediri', 6),
(76, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(77, ' ', 9),
(77, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(77, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(77, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(77, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(77, 'Kepala Kantor Regional II BKN Surabaya', 8),
(77, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(77, 'Sekolah Tinggi Agama Islam Negeri Pamekasan', 6),
(77, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(78, ' ', 9),
(78, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(78, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(78, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(78, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(78, 'Kepala Kantor Regional II BKN Surabaya', 8),
(78, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(78, 'Sekolah Tinggi Agama Islam Negeri Ponorogo', 6),
(78, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(79, ' ', 9),
(79, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(79, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(79, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(79, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(79, 'Kepala Kantor Regional VIII BKN Banjarmasin', 8),
(79, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(79, 'Sekolah Tinggi Agama Islam Negeri Samarinda', 6),
(79, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(80, ' ', 9),
(80, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(80, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(80, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(80, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(80, 'Kepala Kantor Regional VIII BKN Banjarmasin', 8),
(80, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(80, 'Sekolah Tinggi Agama Islam Negeri Palangka Raya', 6),
(80, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(81, ' ', 9),
(81, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(81, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(81, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(81, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(81, 'Kepala Kantor Regional XI BKN Manado', 8),
(81, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(81, 'Sekolah Tinggi Agama Islam Negeri Manado', 6),
(81, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(82, ' ', 9),
(82, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(82, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(82, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(82, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(82, 'Kepala Kantor Regional IV BKN Makassar', 8),
(82, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(82, 'Sekolah Tinggi Agama Islam Negeri Palopo', 6),
(82, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(83, ' ', 9),
(83, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(83, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(83, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(83, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(83, 'Kepala Kantor Regional IV BKN Makassar', 8),
(83, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(83, 'Sekolah Tinggi Agama Islam Negeri Parepare', 6),
(83, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(84, ' ', 9),
(84, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(84, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(84, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(84, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(84, 'Kepala Kantor Regional IV BKN Makassar', 8),
(84, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(84, 'Sekolah Tinggi Agama Islam Negeri Watampone', 6),
(84, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(85, ' ', 9),
(85, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(85, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(85, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(85, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(85, 'Kepala Kantor Regional IV BKN Makassar', 8),
(85, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(85, 'Sekolah Tinggi Agama Islam Negeri Sultan Qaimuddin Kendari', 6),
(85, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(86, ' ', 9),
(86, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(86, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(86, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(86, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(86, 'Kepala Kantor Regional IX BKN Jayapura', 8),
(86, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(86, 'Sekolah Tinggi Agama Islam Negeri Al Fatah Jayapura', 6),
(86, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(87, ' ', 9),
(87, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(87, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(87, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(87, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(87, 'Kepala Kantor Regional IX BKN Jayapura', 8),
(87, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(87, 'Sekolah Tinggi Agama Islam Negeri Sorong', 6),
(87, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(88, ' ', 9),
(88, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(88, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(88, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(88, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(88, 'Kepala Kantor Regional IV BKN Makassar', 8),
(88, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(88, 'Sekolah Tinggi Agama Kristen Negeri Ambon', 6),
(88, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(89, ' ', 9),
(89, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(89, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(89, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(89, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(89, 'Kepala Kantor Regional VIII BKN Banjarmasin', 8),
(89, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(89, 'Sekolah Tinggi Agama Kristen Negeri Palangka Raya', 6),
(89, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(90, ' ', 9),
(90, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(90, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(90, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(90, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(90, 'Kepala Kantor Regional VI BKN Medan', 8),
(90, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(90, 'Sekolah Tinggi Agama Kristen Negeri Tarutung', 6),
(90, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(91, ' ', 9),
(91, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(91, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(91, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(91, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(91, 'Kepala Kantor Regional IV BKN Makassar', 8),
(91, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(91, 'Sekolah Tinggi Agama Kristen Negeri Toraja', 6),
(91, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(92, ' ', 9),
(92, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(92, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(92, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(92, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(92, 'Kepala Kantor Regional IX BKN Jayapura', 8),
(92, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(92, 'Sekolah Tinggi Agama Kristen Negeri Sentani', 6),
(92, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(93, '', 9),
(93, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(93, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(93, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(93, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(93, 'Kepala Kantor Regional XI BKN Manado', 8),
(93, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(93, 'Sekolah Tinggi Agama Kristen Negeri Manado', 6),
(93, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(94, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(94, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(94, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(94, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(94, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(94, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(95, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(95, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(95, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(95, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(95, 'Institut Hindu Dharma Negeri Denpasar', 6),
(95, 'Kepala Kantor Regional X BKN Denpasar', 8),
(95, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(95, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(96, ' ', 9),
(96, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(96, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(96, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(96, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(96, 'Kepala Kantor Regional VIII BKN Banjarmasin', 8),
(96, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(96, 'Sekolah Tinggi Agama Hindu Negeri Tampung Penyang Palangka Raya', 6),
(96, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(97, ' ', 9),
(97, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(97, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(97, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(97, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(97, 'Kepala Kantor Regional X BKN Denpasar', 8),
(97, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(97, 'Sekolah Tinggi Agama Hindu Negeri Gde Pudja Mataram', 6),
(97, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(98, ' ', 9),
(98, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(98, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(98, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(98, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(98, 'Kepala Kantor Regional III BKN Bandung', 8),
(98, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(98, 'Sekolah Tinggi Agama Buddha Negeri Sriwijaya Tangerang', 6),
(98, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(99, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(99, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(99, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(99, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(99, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(99, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(100, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(100, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(100, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(100, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(100, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(100, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(101, 'Balai Diklat Keagamaan Medan', 6),
(101, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(101, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(101, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(101, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(101, 'Kepala Kantor Regional VI BKN Medan', 8),
(101, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(101, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(102, 'Balai Diklat Keagamaan Padang', 6),
(102, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(102, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(102, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(102, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(102, 'Kepala Kantor Regional XII BKN Pekanbaru', 8),
(102, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(102, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(103, 'Balai Diklat Keagamaan Palembang', 6),
(103, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(103, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(103, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(103, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(103, 'Kepala Kantor Regional VII BKN Palembang', 8),
(103, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(103, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(104, 'Badan Litbang dan Diklat Kementerian Agama', 6),
(104, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(104, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(104, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(104, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(104, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(104, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(105, 'Balai Diklat Keagamaan Bandung', 6),
(105, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(105, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(105, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(105, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(105, 'Kepala Kantor Regional III BKN Bandung', 8),
(105, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(105, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(106, 'Balai Diklat Keagamaan Semarang', 6),
(106, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(106, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(106, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(106, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(106, 'Kepala Kantor Regional I BKN Yogyakarta', 8),
(106, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(106, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(107, 'Balai Diklat Keagamaan Surabaya', 6),
(107, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(107, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(107, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(107, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(107, 'Kepala Kantor Regional II BKN Surabaya', 8),
(107, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(107, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(108, 'Balai Diklat Keagamaan Banjarmasin', 6),
(108, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(108, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(108, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(108, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(108, 'Kepala Kantor Regional VIII BKN Banjarmasin', 8),
(108, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(108, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(109, 'Balai Diklat Keagamaan Manado', 6),
(109, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(109, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(109, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(109, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(109, 'Kepala Kantor Regional XI BKN Manado', 8),
(109, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(109, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(110, 'Balai Diklat Keagamaan Makassar', 6),
(110, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(110, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(110, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(110, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(110, 'Kepala Kantor Regional IV BKN Makassar', 8),
(110, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(110, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(111, 'Balai Diklat Keagamaan Ambon', 6),
(111, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(111, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(111, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(111, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(111, 'Kepala Kantor Regional IV BKN Makassar', 8),
(111, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(111, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(112, 'Balai Diklat Keagamaan Denpasar', 6),
(112, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(112, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(112, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(112, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(112, 'Kepala Kantor Regional X BKN Denpasar', 8),
(112, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(112, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(113, 'Balai Diklat Keagamaan Jakarta', 6),
(113, 'Balai Litbang Agama Jakarta', 6),
(113, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(113, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(113, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(113, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(113, 'Kepala Kantor Regional V BKN Jakarta', 8),
(113, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(113, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(114, 'Balai Litbang Agama Semarang', 6),
(114, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2);
INSERT INTO `tbl_tembusan` (`KODE_SATUAN_KERJA`, `Tembusan`, `Tipe`) VALUES
(114, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(114, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(114, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(114, 'Kepala Kantor Regional I BKN Yogyakarta', 8),
(114, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(114, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(115, 'Balai Litbang Agama Makassar', 6),
(115, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(115, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(115, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(115, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(115, 'Kepala Kantor Regional IV BKN Makassar', 8),
(115, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(115, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(116, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(116, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(116, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(116, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(116, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(116, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(117, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(117, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(117, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(117, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(117, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(117, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(118, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(118, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(118, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(118, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(118, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(118, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(119, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(119, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(119, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(119, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(119, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(119, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(120, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(120, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(120, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(120, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(120, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(120, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(121, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(121, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(121, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(121, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(121, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(121, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(122, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(122, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(122, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(122, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(122, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(122, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(123, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(123, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(123, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(123, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(123, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(123, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(124, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(124, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(124, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(124, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(124, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(124, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(125, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(125, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(125, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(125, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(125, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(125, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(126, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(126, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(126, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(126, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(126, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(126, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(127, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(127, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(127, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(127, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(127, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(127, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(128, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(128, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(128, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(128, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(128, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(128, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(129, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(129, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(129, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(129, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(129, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(129, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(130, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(130, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(130, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(130, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(130, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(130, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(131, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(131, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(131, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(131, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(131, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(131, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(132, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(132, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(132, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(132, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(132, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(132, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(133, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(133, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(133, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(133, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(133, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(133, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(134, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(134, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(134, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(134, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(134, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(134, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(135, ' ', 9),
(135, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(135, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(135, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(135, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(135, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(135, 'Sekretariat Jenderal Kementerian Agama', 6),
(135, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(136, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(136, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(136, 'Direktorat Jenderal Pendidikan Islam Kementerian Agama', 6),
(136, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(136, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(136, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(136, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(137, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(137, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(137, 'Direktorat Jenderal Bimbingan Masyarakat Islam Kementerian Agama', 6),
(137, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(137, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(137, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(137, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(138, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(138, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(138, 'Direktorat Jenderal Bimbingan Masyarakat Kristen Kementerian Agama', 6),
(138, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(138, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(138, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(138, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(139, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(139, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(139, 'Direktorat Jenderal Bimbingan Masyarakat Katolik Kementerian Agama', 6),
(139, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(139, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(139, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(139, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(140, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(140, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(140, 'Direktorat Jenderal Bimbingan Masyarakat Hindu Kementerian Agama', 6),
(140, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(140, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(140, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(140, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(141, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(141, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(141, 'Direktorat Jenderal Bimbingan Masyarakat Buddha Kementerian Agama', 6),
(141, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(141, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(141, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(141, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(142, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(142, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(142, 'Direktorat Jenderal Penyelenggaraan Haji dan Umrah Kementerian Agama', 6),
(142, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(142, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(142, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(142, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(143, ' ', 9),
(143, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(143, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(143, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(143, 'Inspektorat Jenderal', 6),
(143, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(143, 'Kepala Kantor Regional V BKN Jakarta', 8),
(143, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(143, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(144, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(144, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(144, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(144, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(144, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(144, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(145, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(145, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(145, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(145, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(145, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(145, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4),
(146, 'Deputi Bidang Informasi Kepegawaian Badan Kepegawaian Negara, Jakarta;', 2),
(146, 'Deputi Bidang SDM Kementerian Pendayagunaan Aparatur Negara dan Reformasi Birokrasi, Jakarta;', 3),
(146, 'Direktur PT. Tabungan dan Asuransi Pegawai Negeri (Persero), Jakarta;', 7),
(146, 'Inspektur Jenderal Kementerian Agama, Jakarta;', 5),
(146, 'Ketua Badan Pemeriksa Keuangan, Jakarta;', 1),
(146, 'Sekretaris Jenderal Kementerian Agama Up Kepala Biro Kepegawaian Sekretariat Jenderal Kementerian Agama, Jakarta;', 4);

-- --------------------------------------------------------

--
-- Table structure for table `tbl_user`
--

CREATE TABLE `tbl_user` (
  `UserID` int(11) NOT NULL,
  `NIP` varchar(18) NOT NULL,
  `UserName` varchar(25) DEFAULT NULL,
  `FullName` varchar(100) DEFAULT NULL,
  `Satker` varchar(25) NOT NULL,
  `Password` varchar(25) NOT NULL,
  `GroupID` varchar(25) DEFAULT NULL,
  `StatusAdmin` int(11) NOT NULL,
  `LastUpdate` datetime DEFAULT NULL ON UPDATE current_timestamp(),
  `LastUser` varchar(100) DEFAULT NULL,
  `LastLogin` datetime DEFAULT NULL ON UPDATE current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `tbl_user`
--

INSERT INTO `tbl_user` (`UserID`, `NIP`, `UserName`, `FullName`, `Satker`, `Password`, `GroupID`, `StatusAdmin`, `LastUpdate`, `LastUser`, `LastLogin`) VALUES
(1, 'admin', NULL, 'administrator', '', 'u1GL+U7ZpwEEUboOXNIaLg==', '01', 0, '2022-06-05 21:46:30', 'admin', '2022-06-05 21:46:30'),
(66, '199512222022032002', NULL, 'DEBBY MUSTIKA WATY, S.T', '135', 'ufBCRgBg8ZF1wsCeZm7MEQ==', '01', 1, '2022-05-21 15:43:58', 'administrator', '2022-05-21 15:43:58'),
(67, '198005022011011007', NULL, 'FIESTYO IMANTA SANTOSO, S.E', '135', 'iV/lMntjCj8kWG37E74tVA==', '02', 0, '2022-05-20 11:34:16', 'administrator', NULL),
(68, '197712242011012005', NULL, 'INDRIA SUKMAWATI., S.Pd', '135', 'x9bj44T0Om4ZLrTc1Hv4Ag==', '02', 0, '2022-05-30 13:07:56', 'administrator', '2022-05-30 13:07:56'),
(69, '197808182009011017', NULL, 'KARNUDIN, S.E., MAB', '135', 'u22mSAaDik9nl/KF4T5yzg==', '02', 0, '2022-05-21 16:06:55', 'administrator', '2022-05-21 16:06:55'),
(70, '196609181988031001', NULL, 'REKO BUDI UTOMO', '135', 'Cqh7p3/k9S0KHrMZHWo6UQ==', '02', 0, '2022-05-20 11:36:54', 'administrator', NULL),
(71, '198007022009011011', NULL, 'WIDI TANURJAYA, S.H', '135', 'QDLt+5oiMdTfxngan/+LFg==', '02', 0, '2022-06-14 10:45:47', 'administrator', '2022-06-14 10:45:47'),
(72, '198609222011011014', NULL, 'SEPTIAN SAPUTRA, S.Kom', '135', 'MzrGJTl2zT7REEIqgX5mOQ==', '05', 0, '2022-06-01 18:33:02', 'administrator', '2022-06-01 18:33:02'),
(73, '197606222009121001', NULL, 'MOH. ASNAWI, S.Ag., M.A.', '143', 'S90HLhrHNzlXOiz7CkCxXQ==', '03', 0, '2022-05-26 21:58:19', 'administrator', '2022-05-26 21:58:19'),
(74, '196611131994031001', NULL, 'H. MOHAMAD ALI IRFAN, SE, MM, M.Ak', '143', 'pOEfFPgnZM+CyPtn2racsA==', '03', 0, '2022-05-21 15:46:24', 'administrator', '2022-05-21 15:46:24'),
(75, '196407191986032001', NULL, 'GIRI REKNOWATI, S.Pd', '135', 'KlZUxmWw4Ek9YPrAdxKg+w==', '02', 0, '2022-05-20 11:40:49', 'administrator', NULL),
(76, '197008302009011006', NULL, 'ALAMSYAH, S.E', '135', 'Qa40oMNT5NBSF+6ICQTlxQ==', '02', 0, '2022-06-06 11:36:52', 'administrator', '2022-06-06 11:36:52'),
(77, '196406022003122001', NULL, 'HJ. AZIEZAH KEBAHYANG, S.H., M.H', '135', 'x7Iv4Ya6XA+XImXE5RIa/g==', '06', 0, '2022-06-01 18:33:43', 'administrator', '2022-06-01 18:33:43'),
(78, '198007202006041003', NULL, 'Dr. NURUDIN, S.Pd.I., M.Si.', '135', 'HLhEGMdfQq8Ef97vniptUA==', '04', 0, '2022-06-02 08:26:49', 'administrator', '2022-06-02 08:26:49'),
(79, '198302202003122001', NULL, 'HANI FEBRIA HANDAYANI, S.H', '135', '9Qw6TD1FeiVhohfO1tRYGQ==', '02', 0, '2022-05-21 09:44:01', 'administrator', '2022-05-21 09:44:01'),
(80, '197608281999031004', NULL, 'PIPIT WAHYUDIN , S.Pd.I', '13', 'AeVM+rTfhlUkZf54prZXqw==', '03', 0, '2022-06-01 10:37:33', 'administrator', '2022-06-01 10:37:33'),
(81, '198002082003121003', NULL, 'H. NUGRAHA STIAWAN, S.Sos.I., M.Ak.', '143', 'eK26sHoLg73UtVIlRo3xQQ==', '03', 0, '2022-05-27 00:09:26', 'administrator', '2022-05-27 00:09:26'),
(82, '197003301998031001', NULL, 'AS\'AD ADI NUGROHO, S.H', '135', 'F5F1tRtLDUpt2pUKONczsA==', '03', 0, '2022-05-20 11:44:16', 'administrator', NULL),
(83, '197810112002122002', NULL, 'DJAMILA BALADRAF, S.Ag, M.H.I', '23', 'AX/dizxwZrHL4eGoaF4vDg==', '03', 0, '2022-05-30 12:48:25', 'administrator', '2022-05-30 12:48:25'),
(84, '196412051993011001', NULL, 'Drs. H. NYAK DIN ACEH, MM', '12', 'M0edVaUq9W2B8qy9sb14xA==', '03', 0, '2022-06-01 18:30:09', 'administrator', '2022-06-01 18:30:09');

-- --------------------------------------------------------

--
-- Table structure for table `tbl_usergroup`
--

CREATE TABLE `tbl_usergroup` (
  `GroupID` varchar(10) NOT NULL,
  `GroupDesc` varchar(50) DEFAULT NULL,
  `LastUpdate` datetime DEFAULT NULL ON UPDATE current_timestamp(),
  `LastUser` varchar(100) DEFAULT NULL
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
-- Indexes for table `tbl_pejabatmst`
--
ALTER TABLE `tbl_pejabatmst`
  ADD PRIMARY KEY (`ID`);

--
-- Indexes for table `tbl_pradpk`
--
ALTER TABLE `tbl_pradpk`
  ADD PRIMARY KEY (`NIP`);

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
-- Indexes for table `tbl_tembusan`
--
ALTER TABLE `tbl_tembusan`
  ADD PRIMARY KEY (`KODE_SATUAN_KERJA`,`Tembusan`,`Tipe`);

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
  MODIFY `id` int(3) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=36;

--
-- AUTO_INCREMENT for table `kasus`
--
ALTER TABLE `kasus`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=25;

--
-- AUTO_INCREMENT for table `tbl_konseptor_satker`
--
ALTER TABLE `tbl_konseptor_satker`
  MODIFY `KODE_SATUAN_KERJA` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=146;

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
  MODIFY `UserID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=85;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
