
create proc sp_getDistributorLiabilitiesSumary
@distribid int
as
begin
		declare @tbl table 
		(
			DistributorId int,
			OrderId int,
			OrderType nvarchar(100),
			Total int,
			DebtDate date,
			OutOfPeriod int,
			InterestRate float,			
			ToMoney int
		)
		declare cur cursor for select id, orderid, debtdate, periodofdebt from DefferredLiabilities
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

			insert into @tbl(DistributorId,OrderId,OrderType,DebtDate,Total,OutOfPeriod,InterestRate,ToMoney)
			values (@distribid, @orderid, @ordertype, @debtdate, @total, @outofperid, @inrate, @tomoney)

			fetch next from cur into @dlid, @orderid, @debtdate, @prioddebt	
		end
		close cur
		deallocate cur
		select * from @tbl
end
go
exec sp_getDistributorLiabilitiesSumary 1