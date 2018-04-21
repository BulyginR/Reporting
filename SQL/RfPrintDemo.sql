select top 1000 * from wh2.rfprintdetail --where prnkey like 'HP%' and doctype = '1' order by adddate desc
where prnkey = 'HP400-00'
order by adddate desc

--update wh2.rfprintdetail set status = 0 where prnkey = 'HP400-00'


--WH2_HP400-00
--update wh2.rfprintdetail set prnkey = 'HP400-00' where serialkey in (806513,802620,797235,802619)

select * from ghost.dbo.SrvPr_REPORTS where reportname in (select reportname from ghost.dbo.SrvPr_RFPRINTDOCS)
select * from ghost.dbo.SrvPr_RFPRINTDOCS where reportname not like '%zebra'
select * from ghost.dbo.SrvPr_RFPRINTDOCTYPES


select top 1000 * from ghost.wh2.SrvPr_PRINTLOG 
where workstation = 'BULYGIN-R'
order by adddate desc