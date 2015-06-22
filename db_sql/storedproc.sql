
drop proc sp_getDistributorLiabilitiesSumary
go
create proc sp_getDistributorLiabilitiesSumary
@distribid int
as
begin
		declare @tbl table 
		(
			DistributorId int,
			OrderId int,
			OrderType nvarchar(100),	-- loại hóa đơn 
			Total int,					-- tổng hóa đơn : tiền
			DebtDate date,				-- ngày nợ
			PeriodOfDebt int,			-- chu kì nợ (thời gian tối đa được phép nợ mà không bị tính lãi)
			OutOfPeriod int,			-- quá hạn : đơn vị ngày
			InterestRate float,			-- lãi suất
			ToMoney int					-- quy thành tiền : 
		)
		declare curorder cursor for select id from Orders where Orders.DistributorID = @distribid
		open curorder
		declare @orid int
		fetch next from curorder into @orid
		while @@FETCH_STATUS = 0
		begin

		declare cur cursor for select DefferredLiabilities.ID, DefferredLiabilities.OrderID,
		 DefferredLiabilities.debtdate, DefferredLiabilities.periodofdebt 
		from DefferredLiabilities where DefferredLiabilities.OrderID = @orid
			open cur
			declare @dlid int
			declare @orderid int
			declare @debtdate date
			declare @prioddebt int

			fetch next from cur into @dlid, @orderid, @debtdate, @prioddebt

		while @@FETCH_STATUS = 0
				begin
					declare @ordertype nvarchar(100)
					declare @outofperid int
					declare @inrate float
					declare @total int
					declare @tomoney int

					select @ordertype = ordertype.OrdType 
					from Orders, ordertype
					where Orders.ID = @orderid and Orders.OrderTypeid = ordertype.id

					set @outofperid = datediff(day,dateadd(day,@prioddebt, @debtdate), getdate())

					select @inrate = max(interestpayable) 
					from InterestRate ir
					where @outofperid >= ir.OutOfPeriod

					set @total = (select sum (od.TotalAmount)
					from OrderDetails od, Orders o
					where od.OrderID = o.ID and od.OrderID = @orderid)

					set @tomoney = (@inrate * @total)/100

					insert into @tbl(DistributorId,OrderId,OrderType,DebtDate, PeriodOfDebt, Total,OutOfPeriod,InterestRate,ToMoney)
					values (@distribid, @orderid, @ordertype, @debtdate,@prioddebt, @total, @outofperid, @inrate, @tomoney)

					fetch next from cur into @dlid, @orderid, @debtdate, @prioddebt	
				end
			close cur
			deallocate cur
			fetch next from curorder into @orid
			end
		close curorder 
		deallocate curorder
		select * from @tbl
end
go
--exec sp_getDistributorLiabilitiesSumary 3
