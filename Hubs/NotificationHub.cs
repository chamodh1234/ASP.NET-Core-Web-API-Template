using Microsoft.AspNetCore.SignalR;

namespace WebApiTemplate.Hubs;

/// <summary>
/// SignalR hub for real-time notifications
/// </summary>
public class NotificationHub : Hub
{
    private readonly ILogger<NotificationHub> _logger;

    public NotificationHub(ILogger<NotificationHub> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Called when a client connects to the hub
    /// </summary>
    public override async Task OnConnectedAsync()
    {
        _logger.LogInformation("Client connected: {ConnectionId}", Context.ConnectionId);
        await base.OnConnectedAsync();
    }

    /// <summary>
    /// Called when a client disconnects from the hub
    /// </summary>
    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        _logger.LogInformation("Client disconnected: {ConnectionId}", Context.ConnectionId);
        await base.OnDisconnectedAsync(exception);
    }

    /// <summary>
    /// Join a group for specific notifications
    /// </summary>
    /// <param name="groupName">Name of the group to join</param>
    public async Task JoinGroup(string groupName)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        _logger.LogInformation("Client {ConnectionId} joined group: {GroupName}", Context.ConnectionId, groupName);
        
        await Clients.Group(groupName).SendAsync("UserJoinedGroup", Context.ConnectionId, groupName);
    }

    /// <summary>
    /// Leave a group
    /// </summary>
    /// <param name="groupName">Name of the group to leave</param>
    public async Task LeaveGroup(string groupName)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        _logger.LogInformation("Client {ConnectionId} left group: {GroupName}", Context.ConnectionId, groupName);
        
        await Clients.Group(groupName).SendAsync("UserLeftGroup", Context.ConnectionId, groupName);
    }

    /// <summary>
    /// Send a message to all connected clients
    /// </summary>
    /// <param name="message">Message to send</param>
    public async Task SendMessageToAll(string message)
    {
        _logger.LogInformation("Broadcasting message from {ConnectionId}: {Message}", Context.ConnectionId, message);
        await Clients.All.SendAsync("ReceiveMessage", Context.ConnectionId, message, DateTime.UtcNow);
    }

    /// <summary>
    /// Send a message to a specific group
    /// </summary>
    /// <param name="groupName">Name of the group</param>
    /// <param name="message">Message to send</param>
    public async Task SendMessageToGroup(string groupName, string message)
    {
        _logger.LogInformation("Sending message to group {GroupName} from {ConnectionId}: {Message}", 
            groupName, Context.ConnectionId, message);
        await Clients.Group(groupName).SendAsync("ReceiveGroupMessage", Context.ConnectionId, groupName, message, DateTime.UtcNow);
    }

    /// <summary>
    /// Send a message to a specific user
    /// </summary>
    /// <param name="userId">User ID to send message to</param>
    /// <param name="message">Message to send</param>
    public async Task SendMessageToUser(string userId, string message)
    {
        _logger.LogInformation("Sending message to user {UserId} from {ConnectionId}: {Message}", 
            userId, Context.ConnectionId, message);
        await Clients.User(userId).SendAsync("ReceiveUserMessage", Context.ConnectionId, message, DateTime.UtcNow);
    }

    /// <summary>
    /// Send a notification to all clients
    /// </summary>
    /// <param name="notification">Notification object</param>
    public async Task SendNotificationToAll(object notification)
    {
        _logger.LogInformation("Broadcasting notification from {ConnectionId}", Context.ConnectionId);
        await Clients.All.SendAsync("ReceiveNotification", notification, DateTime.UtcNow);
    }

    /// <summary>
    /// Send a product update notification
    /// </summary>
    /// <param name="productId">Product ID</param>
    /// <param name="action">Action performed (created, updated, deleted)</param>
    /// <param name="productName">Product name</param>
    public async Task SendProductUpdateNotification(int productId, string action, string productName)
    {
        var notification = new
        {
            Type = "ProductUpdate",
            ProductId = productId,
            Action = action,
            ProductName = productName,
            Timestamp = DateTime.UtcNow
        };

        _logger.LogInformation("Sending product update notification: {Action} - {ProductName}", action, productName);
        await Clients.All.SendAsync("ReceiveProductUpdate", notification);
    }

    /// <summary>
    /// Send a stock alert notification
    /// </summary>
    /// <param name="productId">Product ID</param>
    /// <param name="productName">Product name</param>
    /// <param name="currentStock">Current stock level</param>
    /// <param name="threshold">Stock threshold</param>
    public async Task SendStockAlertNotification(int productId, string productName, int currentStock, int threshold)
    {
        var notification = new
        {
            Type = "StockAlert",
            ProductId = productId,
            ProductName = productName,
            CurrentStock = currentStock,
            Threshold = threshold,
            Timestamp = DateTime.UtcNow
        };

        _logger.LogInformation("Sending stock alert notification: {ProductName} - Stock: {CurrentStock}", productName, currentStock);
        await Clients.Group("Admin").SendAsync("ReceiveStockAlert", notification);
    }

    /// <summary>
    /// Send a system notification
    /// </summary>
    /// <param name="title">Notification title</param>
    /// <param name="message">Notification message</param>
    /// <param name="level">Notification level (info, warning, error)</param>
    public async Task SendSystemNotification(string title, string message, string level = "info")
    {
        var notification = new
        {
            Type = "SystemNotification",
            Title = title,
            Message = message,
            Level = level,
            Timestamp = DateTime.UtcNow
        };

        _logger.LogInformation("Sending system notification: {Title} - {Level}", title, level);
        await Clients.All.SendAsync("ReceiveSystemNotification", notification);
    }
} 