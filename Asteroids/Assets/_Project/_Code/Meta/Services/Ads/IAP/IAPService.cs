using UnityEngine.Purchasing;
using System.Collections.Generic;
using _Project._Code.Meta.Services.Ads;
using _Project._Code.Meta.Services.Ads.IAP;
using UnityEngine;
using Zenject;

public class IAPService : IStoreListener, IInitializable, IIAPService
{
    private  IStoreController _storeController;
    private  IExtensionProvider _storeExtensionProvider;
    
    private IAdsToggle _adsToggle;
    
    private string NoAdsProductId = "remove_ads";

    public IAPService(IAdsToggle adsToggle)
    {
        _adsToggle = adsToggle;
    }

    public void Initialize()
    {
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        builder.AddProduct(NoAdsProductId, ProductType.NonConsumable);
        UnityPurchasing.Initialize(this, builder);
    }

    private bool IsInitialized()
    {
        return _storeController != null && _storeExtensionProvider != null;
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        _storeController = controller;
        _storeExtensionProvider = extensions;

        Debug.Log("Unity IAP успешно инициализирован");
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.LogError($"Ошибка инициализации Unity IAP: {error}");
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        Debug.LogError($"Ошибка инициализации Unity IAP: {error}\n {message}");
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        if (args.purchasedProduct.definition.id == NoAdsProductId)
        {
            Debug.Log("Покупка отключения рекламы завершена!");
            _adsToggle.DisableAds();
            return PurchaseProcessingResult.Complete;
        }

        Debug.LogError("Неизвестный продукт был куплен.");
        return PurchaseProcessingResult.Complete;
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.LogError($"Ошибка покупки {product.definition.id}: {failureReason}");
    }

    public void BuyNoAds()
    {
        if (IsInitialized())
        {
            _storeController.InitiatePurchase(NoAdsProductId);
        }
        else
        {
            Debug.LogError("Unity IAP не инициализирован!");
        }
    }
}