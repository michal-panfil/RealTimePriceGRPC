RealTimePriceGRPC:
It is small demo of gRPC service which has 2 methods. 

GetBasePrice => returns object that contain ID and price
GetRealTimePrices = > returns objects as a stream every 1 sek;

It is nothing sofisticate but helped me to understud gRPC idea.

Thank you!

Proto:

syntax = "proto3";

option csharp_namespace = "PriceGrpcService";

package price;

service PriceProvider{
	rpc GetBasePrice(PriceRequest) returns (PriceReply);
	rpc GetRealTimePrices(PriceRequest) returns(stream PriceReply);

}

message PriceRequest{
int32 id =1;
}

message PriceReply{
	int32 id = 1;
	string price = 2;
}