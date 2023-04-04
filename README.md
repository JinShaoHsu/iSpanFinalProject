# 期中專題作品
## 使用ASP.NET MVC5開發後台 資料庫使用SQL Server
使用三層式架構撰寫並使用Entity Framework跟LinQ連線及操作資料庫 
##
我做的部分是會員管理 共有下述功能
* 列出所有會員
* 註冊新會員
* 編輯會員資料
* 刪除會員
* 瀏覽會員
##
GET /Members/Index
這個方法會列出所有會員的資訊。你可以使用以下的 query string 參數來篩選會員：

Id：會員的 ID
Account：會員的帳號
pageNumber：頁碼，預設值為 1
```
public ActionResult Index(int? Id, string Account, int pageNumber = 1)
{
    // 篩選條件
    ViewBag.Account = Account;
    ViewBag.CategoryId = Id;

    // 取得分頁資料
    IPagedList<Member> pagedData = GetPagedProducts(Id, Account, pageNumber);

    return View(pagedData);
}
```
GetPagedProducts 方法
這個方法會依照給定的篩選條件，回傳分頁後的會員資料。

```
private IPagedList<Member> GetPagedProducts(int? Id, string Account, int pageNumber)
{
    int pageSize = 3;

    var query = db.Members.Include("Avatar").OrderBy(x => x.id);

    return query.ToPagedList(pageNumber, pageSize);
}
```
註冊會員
GET /Members/Register
這個方法會回傳一個表單，供使用者輸入註冊資訊。

```
public ActionResult Register()
{
    return View();
}
```
POST /Members/Register
這個方法會處理使用者提交的註冊表單。如果提交的資料不符合格式，這個方法會重新顯示註冊表單，讓使用者修改資訊。

如果註冊成功，會轉向到註冊成功的頁面。

```
[HttpPost]
public ActionResult Register(RegisterVM model)
{
    if (!ModelState.IsValid)
    {
        return View(model);
    }

    var service = new MemberService(repo);

    (bool IsSuccess, string ErrorMessage) response = service.CreateNewMember(model.ToRequestDto());

    if (response.IsSuccess)
    {
        return View("RegisterConfirm");
    }
    else
    {
        ModelState.AddModelError(string.Empty, response.ErrorMessage);
        return View(model);
    }
}
```
編輯會員資料
GET /Members/EditProfile/{id}
這個方法會回傳一個表單，供使用者編輯會員資料。

```
[HttpPost]
public ActionResult EditProfile(EditProfileVM model)
{
    //string currentUserAccount = User.Identity.Name;

    if (ModelState.IsValid == false)
    {
        return View(model);
    }

    try
    {
        memberService.UpdateProfile(model.ToEditProfileDTO());
    }
    catch (Exception ex)
    {
        ModelState.AddModelError(string.Empty, ex.Message);
    }

    if (ModelState.IsValid == true)
    {
        return RedirectToAction("Index");
    }
    else
    {
        return View(model);
    }
}
```
這個方法會從表單中取得使用者編輯後的資料，並透過 memberService 來更新會員資料。如果更新成功，則會重定向到 Index 頁面，否則會重新顯示編輯頁面並顯示錯誤訊息。

刪除帳號
GET /Members/DeleteAccount/{id}
這個方法會回傳一個確認刪除帳號的頁面，使用者可以在這個頁面中確認是否要刪除帳號。

這個表單可以透過以下的 HttpPost 方法來提交：

```
[HttpPost]
public ActionResult DeleteAccount(EditProfileVM model)
{
    if (ModelState.IsValid == false)
    {
        return View(model);
    }

    try
    {
        memberService.DeleteAccount(model.ToEditProfileDTO());
    }
    catch (Exception ex)
    {
        ModelState.AddModelError(string.Empty, ex.Message);
    }

    if (ModelState.IsValid == true)
    {
        return RedirectToAction("Index");
    }
    else
    {
        return View(model);
    }
}

```
這個方法會從表單中取得使用者的確認，如果使用者確認要刪除帳號，則會透過 memberService 刪除該會員的帳號，並重定向到 Index 頁面。如果刪除失敗，則會重新顯示確認刪除帳號的頁面並顯示錯誤訊息。

其他說明
本專案使用 ASP.NET MVC 5 和 Entity Framework 6，資料庫使用 SQL Server。在使用前，請確認已安裝相關套件，並修改 Web.config 中的資料庫連線字串。
