using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MvcTutorialLinq.Models;

namespace MvcTutorialLinq.Controllers
{
    public class HeroController : Controller
    {
        private static readonly List<HeroName> _heroNames = new List<HeroName>()
        {
            new HeroName(){ HeroId = 1, Name="索爾" },
            new HeroName(){ HeroId = 2, Name="鋼鐵人" },
            new HeroName(){ HeroId = 3, Name="美國隊長" },
            new HeroName(){ HeroId = 4, Name="黑寡婦" },
            new HeroName(){ HeroId = 5, Name="驚奇隊長" },
        };

        private static readonly List<HeroWithoutName> _noNameHeroes = new List<HeroWithoutName>()
        {
            new HeroWithoutName(){Id = 1, Atk=20, Hp=50},
            new HeroWithoutName(){Id = 2, Atk=15, Hp=60},
            new HeroWithoutName(){Id = 3, Atk=20, Hp=45},
            new HeroWithoutName(){Id = 4, Atk=30, Hp=55},
            new HeroWithoutName(){Id = 5,Atk=40, Hp=100},
        };

        private static readonly List<HeroWithName> _heroes = new List<HeroWithName>()
        {
            new HeroWithName(){Id = 1, Name="索爾", Atk=20, Hp=50},
            new HeroWithName(){Id = 2, Name="鋼鐵人", Atk=15, Hp=60},
            new HeroWithName(){Id = 3, Name="美國隊長", Atk=20, Hp=45},
            new HeroWithName(){Id = 4, Name="黑寡婦", Atk=30, Hp=55},
            new HeroWithName(){Id = 5, Name="驚奇隊長",Atk=40, Hp=100},
        };

        public ActionResult LinqTest()
        {
            var attack20 = _heroes
                .Where(hero => hero.Atk > 20) // 還是 IEnumerable
                .ToList(); // ToList 實體
            var item = _heroes.FirstOrDefault(x => x.Atk > 15 && x.Hp > 50);
            var eric = _heroes.FirstOrDefault(x => x.Name == "Eric");
            var hplist = _heroes.OrderByDescending(x => x.Hp);
            var item2 = _heroes.Max(x => x.Atk);
            var item3 = _heroes.Sum(x => x.Hp);

            var joinExample = // 還是 IEnumerable (下訂單)
                from hero in _noNameHeroes
                join name in _heroNames
                on hero.Id equals name.HeroId
                select new HeroWithName
                {
                    Id = hero.Id,
                    Name = name.Name,
                    Atk = hero.Atk,
                    Hp = hero.Hp
                };
            var heroList = joinExample.ToList(); // 變成 List (商品實際到手) 

            return RedirectToAction(nameof(Index));
        }

        // GET: HeroController
        public ActionResult Index()
        {
            return View(_heroes);
        }

        // GET: HeroController/Details/5
        public ActionResult Details(int id)
        {
            var hero = _heroes.FirstOrDefault(x => x.Id == id);
            if (hero == null)
            {
                return NotFound();
            }
            return View(hero);
        }

        // GET: HeroController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HeroController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HeroWithName model)
        {
            try
            {
                if (_heroes.Any(x => x.Id == model.Id))
                {
                    throw new Exception($"ID={model.Id} 重複了");
                }
                _heroes.Add(model);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                throw;
            }
        }

        // GET: HeroController/Edit/5
        public ActionResult Edit(int id)
        {
            var hero = _heroes.FirstOrDefault(x => x.Id == id);
            if (hero == null)
            {
                return NotFound();
            }
            return View(hero);
        }

        // POST: HeroController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, HeroWithName model)
        {
            var hero = _heroes.FirstOrDefault(x => x.Id == id);
            try
            {
                if (hero == null)
                {
                    return NotFound();
                }

                hero.Name = model.Name;
                hero.Atk = model.Atk;
                hero.Hp = model.Hp;
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(hero);
            }
        }

        // GET: HeroController/Delete/5
        public ActionResult Delete(int id)
        {
            var hero = _heroes.FirstOrDefault(x => x.Id == id);
            if (hero == null)
            {
                return NotFound();
            }
            return View(hero);
        }

        // POST: HeroController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            var hero = _heroes.FirstOrDefault(x => x.Id == id);
            try
            {
                if (hero == null)
                {
                    return NotFound();
                }

                _heroes.Remove(hero);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(hero);
            }
        }
    }
}
